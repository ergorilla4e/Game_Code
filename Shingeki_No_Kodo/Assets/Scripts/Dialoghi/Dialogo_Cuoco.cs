using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Dialogo_Cuoco : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text timeText;
    private string[] dialogue;
    private int index;

    public float wordSpeed;
    public bool playerIsCloser;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && playerIsCloser)
        {
            if(dialoguePanel.activeInHierarchy)
            {

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)    
    {
        if(other.CompareTag("Player"))
        {
            playerIsCloser = true;
        }    
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsCloser = false;
        }
    }

}
