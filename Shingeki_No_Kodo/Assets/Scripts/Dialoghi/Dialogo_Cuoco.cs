using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Dialogo_Cuoco : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueSprite;
    [SerializeField] private TextMeshProUGUI textComoponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float speedText;
    [SerializeField] private Clock clock;

    [SerializeField] private ShopTemplate[] ShopPanels;

    public TMP_Text[] nuemeroBBTPosseduti;

    [SerializeField] private bool firstInteraction;
    [SerializeField] private GameObject windowPanel;
    [SerializeField] private GameObject[] bubbleTeaPrefab;

    [SerializeField] private Transform[] spawnPoints;

    public int[] arrayOfBubbleTea;
    private int grandezzaArrayBBT = 10;

    private int bubbleTeaScelto = 0;

    private int _index;
    private bool playerIsCloser;

    private int numeroDialogo;

    private bool[] isPositionOccupied;

    private int countPositionOccupied = 0;

    public int velocitaCuoco = 3;

    private void Start()
    {
        textComoponent.text = string.Empty;
        dialogueSprite.SetActive(true);
        firstInteraction = true;
        numeroDialogo = 0;
        isPositionOccupied = new bool[spawnPoints.Length];

        arrayOfBubbleTea = new int[grandezzaArrayBBT];

        for(int i = 0; i < arrayOfBubbleTea.Length; i++)
        {
            arrayOfBubbleTea[i] = 1;
        }
    }

    private void Update()
    {
        if (firstInteraction)
        {
            if (Input.GetMouseButtonDown(0) && playerIsCloser)
            {
                if (textComoponent.text == lines[_index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComoponent.text = lines[_index];
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) && playerIsCloser)
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    StopAllCoroutines();
                    textComoponent.text = lines[_index];
                }
                else
                {
                    clock.SetTimeIsRunning(false);
                    dialogueSprite.SetActive(false);
                    dialoguePanel.SetActive(true);
                    StartDialog();
                }
            }

            if (!dialoguePanel.activeSelf)
            {
                clock.SetTimeIsRunning(true);
            }

            if (!playerIsCloser)
            {
                clock.SetTimeIsRunning(true);
                StopAllCoroutines();
                textComoponent.text = string.Empty;
                dialoguePanel.SetActive(false);
            }

            if (numeroDialogo == lines.Length - 1 && !dialoguePanel.activeSelf)
            {
                firstInteraction = false;
            }

        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space) && playerIsCloser && countPositionOccupied < 5)
            {
                windowPanel.SetActive(true);
            }
        }


        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (isPositionOccupied[i])
            {
                // Verifica se l'oggetto istanziato è null
                if (spawnPoints[i].childCount == 0)
                {
                    countPositionOccupied--;
                    ResetPosition(i); // Imposta la posizione come non occupata
                }
            }
        }
         
    }

    private void ChoseBBT()
    {       
        StartCoroutine(IstantiateAfterSeconds(bubbleTeaScelto));
    }

    public void OnButtonClick(GameObject clickedButtonObject)
    {
        if (clickedButtonObject.name == "BubbleTea1")
        {
            bubbleTeaScelto = 0;
            ChoseBBT();
        }
        else if (clickedButtonObject.name == "BubbleTea2")
        {
            bubbleTeaScelto = 1;
            ChoseBBT();
        }
        else if (clickedButtonObject.name == "BubbleTea3")
        {
            bubbleTeaScelto = 2; 
            ChoseBBT();
        }
        else if (clickedButtonObject.name == "BubbleTea4")
        {
            bubbleTeaScelto = 3;
            ChoseBBT();
        }
        else if (clickedButtonObject.name == "BubbleTea5")
        {
            bubbleTeaScelto = 4;
            ChoseBBT();
        }
        else if (clickedButtonObject.name == "BubbleTea6")
        {
            bubbleTeaScelto = 5;
            ChoseBBT();
        }
        else if (clickedButtonObject.name == "BubbleTea7")
        {
            bubbleTeaScelto = 6;
            ChoseBBT();
        }
        else if (clickedButtonObject.name == "BubbleTea8")
        {
            bubbleTeaScelto = 7;
            ChoseBBT();
        }
        else if (clickedButtonObject.name == "BubbleTea9")
        {
            bubbleTeaScelto = 8;
            ChoseBBT();
        }
        else
        {
            bubbleTeaScelto = 9;
            ChoseBBT();
        }
    }

    IEnumerator IstantiateAfterSeconds(int bubbleTeaScelto)
    {
        yield return new WaitForSeconds(velocitaCuoco);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!isPositionOccupied[i] && arrayOfBubbleTea[bubbleTeaScelto] > 0)
            {
                arrayOfBubbleTea[bubbleTeaScelto]--;
                countPositionOccupied++;
                ShopPanels[bubbleTeaScelto].possedutiText.text = "Posseduti:" + arrayOfBubbleTea[bubbleTeaScelto];
                nuemeroBBTPosseduti[bubbleTeaScelto].text = "" + arrayOfBubbleTea[bubbleTeaScelto];
                GameObject bubbleTeaInstance = Instantiate(bubbleTeaPrefab[bubbleTeaScelto], spawnPoints[i].position, Quaternion.identity);
                bubbleTeaInstance.transform.parent = spawnPoints[i]; // Imposta il punto di spawn come genitore dell'istanza Bubble Tea
                isPositionOccupied[i] = true;
                break;
            }
        }
    }

    public void ResetPosition(int index)
    {
        isPositionOccupied[index] = false;
    }

    public void StartDialog()
    {
        _index = 0;

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[_index].ToCharArray())
        {
            textComoponent.text += c;
            yield return new WaitForSeconds(speedText);
        }
    }

    public void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            numeroDialogo++;
            _index++;
            textComoponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsCloser = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsCloser = false;
            textComoponent.text = string.Empty;
        }
    }

}
