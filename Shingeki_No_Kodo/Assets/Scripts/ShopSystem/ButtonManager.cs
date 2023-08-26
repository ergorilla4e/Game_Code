using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Action actionButtonOpen;

    [SerializeField] private GameObject negozio;

    private void Start()
    {
        actionButtonOpen = OpenAndClose;
    }

    public void OpenAndClose()
    {
        if(negozio.gameObject.activeSelf)
        {
            negozio.gameObject.SetActive(false);
        }
        else
        {
            negozio.gameObject.SetActive(true);
        }
    }

}
