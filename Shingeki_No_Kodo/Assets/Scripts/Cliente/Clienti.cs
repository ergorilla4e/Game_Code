using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Clienti : MonoBehaviour
{

    [SerializeField] private Transform[] Points;
    [SerializeField] private float MoveSpeed;

    public enum iconType
    {
        happy,
        neutral,
        angry
    }

    [SerializeField] private GameObject balloon;
    [SerializeField] private GameObject happyIconSprite;
    [SerializeField] private GameObject neutralIconSprite;
    [SerializeField] private GameObject angryIconSprite;
    [SerializeField] private Clock clock;

    [SerializeField] private ShopManager shopManager;

    private int _indexPoint = 0;
    private float _tempoAtteso = 0;
    private bool playerIsCloser;

    private int pazienzaHappy = 8;
    private int pazienzaNeutral = 13;
    private int pazienzaAngry = 18;

    [SerializeField] private RandomSpawner spawner;

    private GameObject _clientPrefab;

    [SerializeField] private GameObject[] _BubbleTea_n;
    private int bubbleTeaScelto;

    private bool keyIsPressed = false;

    private int paga;

    private bool consegnaBBT = false;

    [SerializeField] private HumorBar UI_HumorBar;

    private bool Losing = false;

    private void Start()
    {
        this.transform.position = this.Points[_indexPoint].transform.position;

        balloon.SetActive(false);
        happyIconSprite.SetActive(false);
        neutralIconSprite.SetActive(false);
        angryIconSprite.SetActive(false);

        bubbleTeaScelto = RandomBubbleTea();

        spawner = FindObjectOfType<RandomSpawner>(true);
        clock = FindObjectOfType<Clock>(true);
        shopManager = FindObjectOfType<ShopManager>(true);
        UI_HumorBar = FindObjectOfType<HumorBar>(true);

        for (int i = 0; i < _BubbleTea_n.Length; i++)
        {
            _BubbleTea_n[i].SetActive(false);
        }

        this.paga = 9;

    }

    void Update()
    {
        #region SEGUI_PERCORSO

        //if con orologio se timer fermo

        if (clock.GetTimeIsRunning())
        {
            if (_indexPoint < this.Points.Length && _tempoAtteso < pazienzaAngry - clock.GetContatoreGiorni())
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, this.Points[_indexPoint].transform.position, this.MoveSpeed * Time.deltaTime);

                if (this.transform.position == this.Points[_indexPoint].transform.position)
                {
                    this._indexPoint++;
                    _tempoAtteso = 0;
                }
            }
            else if (_indexPoint > 0 && _tempoAtteso >= pazienzaAngry - clock.GetContatoreGiorni())
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, this.Points[_indexPoint - 1].transform.position, this.MoveSpeed * Time.deltaTime);

                if (this.transform.position == this.Points[_indexPoint - 1].transform.position)
                {
                    this._indexPoint--;
                }

            }

            if (_indexPoint == Points.Length)
            {
                _tempoAtteso += Time.deltaTime;
            }
        }
        else
        {
            this.transform.position = this.transform.position; // setto la posizione del cliente a se stesso, in questo modo sembra che stia fermo 
        }


        #endregion


        #region DIALOGO_CLIENTE_GIOCATORE

        if (playerIsCloser && !keyIsPressed && !consegnaBBT)
        {
            chooseIcon();
            balloon.SetActive(true);
        }
        else if (!playerIsCloser && !consegnaBBT)
        {
            keyIsPressed = false;

            balloon.SetActive(false);
            happyIconSprite.SetActive(false);
            neutralIconSprite.SetActive(false);
            angryIconSprite.SetActive(false);
            _BubbleTea_n[bubbleTeaScelto].SetActive(false);
        }

        if (playerIsCloser && Input.GetKeyDown(KeyCode.Space))
        {
            keyIsPressed = true;
            balloon.SetActive(true);
            _BubbleTea_n[bubbleTeaScelto].SetActive(true);

            happyIconSprite.SetActive(false);
            neutralIconSprite.SetActive(false);
            angryIconSprite.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (playerIsCloser && !consegnaBBT)
            {
                consegnaBBT = OnBubbleTeaButtonPressed(GetBubbleTea());

                if (consegnaBBT)
                {
                    balloon.SetActive(true);
                    _BubbleTea_n[bubbleTeaScelto].SetActive(true);

                    happyIconSprite.SetActive(false);
                    neutralIconSprite.SetActive(false);
                    angryIconSprite.SetActive(false);
                }
                else
                {
                    Debug.Log("Non hai l'oggetto nell'inventario");
                }
            }
        }
        #endregion

    }

    public void chooseIcon()
    {
        if (_tempoAtteso <= pazienzaHappy - clock.GetContatoreGiorni())
        {
            getIconSprite(iconType.happy);
        }
        else if (_tempoAtteso <= pazienzaNeutral - clock.GetContatoreGiorni() && _tempoAtteso > pazienzaHappy - clock.GetContatoreGiorni())
        {
            getIconSprite(iconType.neutral);
        }
        else if (_tempoAtteso > pazienzaNeutral - clock.GetContatoreGiorni())
        {
            getIconSprite(iconType.angry);
        }
    }
    public void getIconSprite(iconType icon)
    {
        switch (icon)
        {
            default:
            case iconType.happy:
                happyIconSprite.SetActive(true);
                break;
            case iconType.neutral:
                neutralIconSprite.SetActive(true);
                break;
            case iconType.angry:
                angryIconSprite.SetActive(true);
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsCloser = true;
        }

        if (other.CompareTag("Door") && _tempoAtteso > pazienzaAngry - clock.GetContatoreGiorni())
        {
            if (!consegnaBBT)
            {
                UI_HumorBar.addHumor(-3 * clock.GetContatoreGiorni());
                UI_HumorBar.UpdateGraphics();

                if (UI_HumorBar.GetHumor() <= 0 && !Losing)
                {
                    Losing = true;
                    clock.goToLoseGameScene();
                }
            }
            Destroy(this.transform.parent.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsCloser = false;
        }
    }

    public void OnDestroy()
    {
        _clientPrefab = GameObject.Find("Cliente1(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(0);
        }

        _clientPrefab = GameObject.Find("Cliente2(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(1);
        }

        _clientPrefab = GameObject.Find("Cliente3(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(2);
        }

        _clientPrefab = GameObject.Find("Cliente4(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(3);
        }

        _clientPrefab = GameObject.Find("Cliente5(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(4);
        }

        _clientPrefab = GameObject.Find("Cliente6(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(5);
        }

        _clientPrefab = GameObject.Find("Cliente7(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(6);
        }

        _clientPrefab = GameObject.Find("Cliente8(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(7);
        }

        _clientPrefab = GameObject.Find("Cliente9(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(8);
        }

        _clientPrefab = GameObject.Find("Cliente10(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(9);
        }
        _clientPrefab = GameObject.Find("Cliente11(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(10);
        }

        _clientPrefab = GameObject.Find("Cliente12(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(11);
        }

        _clientPrefab = GameObject.Find("Cliente13(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(12);
        }

        _clientPrefab = GameObject.Find("Cliente14(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(13);
        }

        _clientPrefab = GameObject.Find("Cliente15(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(14);
        }
        _clientPrefab = GameObject.Find("Cliente16(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(15);
        }

        _clientPrefab = GameObject.Find("Cliente17(Clone)");

        if (_clientPrefab == null)
        {
            spawner._indiciOccupati.Remove(16);
        }
        
    }

    public int RandomBubbleTea()
    {
        return UnityEngine.Random.Range(0, _BubbleTea_n.Length);
    }

    public int Pagamento()
    {
        if (_tempoAtteso <= pazienzaHappy - clock.GetContatoreGiorni())
        {
            UI_HumorBar.addHumor(7 - clock.GetContatoreGiorni());
            UI_HumorBar.UpdateGraphics();
            return paga = 9;
        }
        else if (_tempoAtteso <= pazienzaNeutral - clock.GetContatoreGiorni() && _tempoAtteso > pazienzaHappy - clock.GetContatoreGiorni())
        {
            UI_HumorBar.addHumor(4 - clock.GetContatoreGiorni());
            UI_HumorBar.UpdateGraphics();
            return paga = 6;
        }
        else
        {
            UI_HumorBar.addHumor( - clock.GetContatoreGiorni());
            UI_HumorBar.UpdateGraphics();

            if (UI_HumorBar.GetHumor() <= 0)
            {
                clock.goToLoseGameScene();
            }

            return paga = 3;
        }
    }

    public bool OnBubbleTeaButtonPressed(GameObject BBT_Cliente)
    {
        Debug.Log("Sto dando il BBT");

        for (int i = 0; i < Inventory.Instance.items.Count; i++)
        {
            if (Inventory.Instance.items[i].name == BBT_Cliente.name)
            {
                Inventory.Instance.RemoveItem(Inventory.Instance.items[i]);

                paga = Pagamento();

                shopManager.Coins += paga;

                _BubbleTea_n[bubbleTeaScelto].SetActive(false);

                happyIconSprite.SetActive(false);
                neutralIconSprite.SetActive(false);
                angryIconSprite.SetActive(false);

                Debug.Log("Transazione RIUSCITA");
                return true;
            }

        }
        return false;

    }

    public GameObject GetBubbleTea()
    {
        return _BubbleTea_n[bubbleTeaScelto];
    }

}

