using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Cliente_Movements : MonoBehaviour
{

    [SerializeField] private Transform[] Points;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float Pazienza = 2;

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

    private int _indexPoint = 0;
    private float _tempoAtteso = 0;
    private bool playerIsCloser;

    [SerializeField] private RandomSpawner spawner;

    private GameObject _clientPrefab;

    [SerializeField] private GameObject[] _BubbleTea_n;
    private int bubbleTeaScelto;

    private bool keyIsPressed = false;

    private int paga;
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

        for (int i = 0; i < _BubbleTea_n.Length; i++)
        {

            _BubbleTea_n[i].SetActive(false);

        }
        paga = 9;
    }

    void Update()
    {
        #region SEGUI_PERCORSO

        //if con orologio se timer fermo

        if (clock.GetTimeIsRunning())
        {
            if (_indexPoint < this.Points.Length && _tempoAtteso < Pazienza)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, this.Points[_indexPoint].transform.position, this.MoveSpeed * Time.deltaTime);

                if (this.transform.position == this.Points[_indexPoint].transform.position)
                {
                    this._indexPoint++;
                    _tempoAtteso = 0;
                }
            }
            else if (_indexPoint > 0 && _tempoAtteso >= Pazienza)
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

        if (playerIsCloser && !keyIsPressed)
        {
            chooseIcon();
            balloon.SetActive(true);



        }
        else if (!playerIsCloser)
        {
            keyIsPressed = false;

            balloon.SetActive(false);
            happyIconSprite.SetActive(false);
            neutralIconSprite.SetActive(false);
            angryIconSprite.SetActive(false);
            _BubbleTea_n[bubbleTeaScelto].SetActive(false);
        }

        if (playerIsCloser && Input.GetKey(KeyCode.Space))
        {
            keyIsPressed = true;
            paga = Pagamento(paga);
            Debug.Log(paga);
            balloon.SetActive(true);
            _BubbleTea_n[bubbleTeaScelto].SetActive(true);

            happyIconSprite.SetActive(false);
            neutralIconSprite.SetActive(false);
            angryIconSprite.SetActive(false);

        }

        #endregion

    }

    public void chooseIcon()
    {
        if (_tempoAtteso <= 7)
        {
            getIconSprite(iconType.happy);
        }
        else if (_tempoAtteso <= 14 && _tempoAtteso > 7)
        {
            getIconSprite(iconType.neutral);
        }
        else if (_tempoAtteso > 14)
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

        if (other.CompareTag("Door") && _tempoAtteso > 21)
        {
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
    }

    public int RandomBubbleTea()
    {
        return UnityEngine.Random.Range(0, _BubbleTea_n.Length);
    }

    public int Pagamento(int paga)
    {
        if (_tempoAtteso <= 7)
        {
            return paga = 9;
        }
        else if (_tempoAtteso <= 14 && _tempoAtteso > 7)
        {
            return paga = 6;
        }
        else
        {
            return paga = 3;
        }

    }
}

