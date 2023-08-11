using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour
{
    [SerializeField] private Clock clock;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Image image_border;
    [SerializeField] private Image image_intern;

    void FixedUpdate()
    {

        if (clock.GetTimeRemaining() >= 360 && clock.GetTimeRemaining() <= 380)
        {
            dialogText.gameObject.SetActive(true);
            image_border.gameObject.SetActive(true);
            image_intern.gameObject.SetActive(true);

            displayDialog("Il negozio apre alle 08:00");
        }
        else if(clock.GetTimeRemaining() >380 && clock.GetTimeRemaining() <= 480)
        {
            dialogText.gameObject.SetActive(false);
            image_border.gameObject.SetActive(false);
            image_intern.gameObject.SetActive(false);
        }
        else if(clock.GetTimeRemaining() > 480 && clock.GetTimeRemaining() <= 486)
        {
            dialogText.gameObject.SetActive(true);
            image_border.gameObject.SetActive(true);
            image_intern.gameObject.SetActive(true);

            displayDialog("Il negozio e'aperto e chiude alle 20:00");
        }
        else if (clock.GetTimeRemaining() >486 && clock.GetTimeRemaining() <= 1200)
        {
            dialogText.gameObject.SetActive(false);
            image_border.gameObject.SetActive(false);
            image_intern.gameObject.SetActive(false);
        }
        else if (clock.GetTimeRemaining() > 1200 && clock.GetTimeRemaining() <= 1220)
        {
            dialogText.gameObject.SetActive(true);
            image_border.gameObject.SetActive(true);
            image_intern.gameObject.SetActive(true);

            displayDialog("Il negozio e'chiuso");
        }
        else
        {
            dialogText.gameObject.SetActive(false);
            image_border.gameObject.SetActive(false);
            image_intern.gameObject.SetActive(false);
        }
        
    }

    void displayDialog(string text)
    {
        dialogText.text = text;
    }

}
