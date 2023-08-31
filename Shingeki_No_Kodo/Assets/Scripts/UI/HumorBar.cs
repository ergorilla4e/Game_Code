using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumorBar : MonoBehaviour
{
    [SerializeField] private Image fillableBar;

    private int currentHumor = 30;
    private int maxHumor = 100;

    private void Start()
    {
        UpdateGraphics();
    }

    public void UpdateGraphics()
    {
        this.fillableBar.fillAmount = (float) currentHumor / maxHumor;
    }

    public int GetHumor()
    {
        return currentHumor;
    }

    public void addHumor(int Humor)
    {
        this.currentHumor += Humor;

        if(this.currentHumor > maxHumor)
        {
            currentHumor = maxHumor; 
        }
    }

}
