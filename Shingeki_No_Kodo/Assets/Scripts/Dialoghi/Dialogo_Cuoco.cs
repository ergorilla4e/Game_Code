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
    [SerializeField] private TextMeshProUGUI textComoponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float speedText;

    private int _index;
    private bool playerIsCloser;

    private void Start()
    {
        textComoponent.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerIsCloser)
        {
            if(textComoponent.text == lines[_index])
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
                dialoguePanel.SetActive(true);
                StartDialog();
            }
        }

        if(!playerIsCloser)
        {
            StopAllCoroutines();
            textComoponent.text = string.Empty;
            dialoguePanel.SetActive(false);
        }
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
