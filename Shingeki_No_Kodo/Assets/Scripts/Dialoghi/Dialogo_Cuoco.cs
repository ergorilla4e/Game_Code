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

    [SerializeField] private bool firstInteraction;
    [SerializeField] private GameObject windowPanel;
    [SerializeField] private GameObject[] bubbleTeaPrefab;

    //[SerializeField] private GameObject[] buttonsBBT;

    private int bubbleTeaScelto = 0;

    private int _index;
    private bool playerIsCloser;

    private int numeroDialogo;

    private void Start()
    {
        textComoponent.text = string.Empty;
        dialogueSprite.SetActive(true);
        firstInteraction = true;
        numeroDialogo = 0;

        //foreach (GameObject buttonObject in buttonsBBT)
        //{
        //    UnityEngine.UI.Button button = buttonObject.GetComponent<UnityEngine.UI.Button>();
        //    if (button != null)
        //    {
        //        button.onClick.AddListener(() => OnButtonClick(buttonObject));
        //    }
        //}
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

            if (numeroDialogo == 2 && !dialoguePanel.activeSelf)
            {
                firstInteraction = false;
            }

        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space) && playerIsCloser)
            {
                windowPanel.SetActive(true);
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
        yield return new WaitForSeconds(0);

        Instantiate(bubbleTeaPrefab[bubbleTeaScelto], new Vector3(-7.5f, -7.6f, 0f), Quaternion.identity);
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
