using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour

{
    public int Coins;
    public TMP_Text CoinUI; //player Gold
    public ShopItemSO[] ShopItemSO;
    public ShopTemplate[] ShopPanels;
    public GameObject[] ShopPanelGO;
    public Button[] myPurchase;




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ShopItemSO.Length; i++)
            ShopPanelGO[i].SetActive(true);
        CoinUI.text = "Coins:" + Coins.ToString();
        LoadPanel();
        
    }


    public void CheckPurcheseable()
    {
        for (int i = 0; i < ShopItemSO.Length; i++)
        {
            if (Coins >= ShopItemSO[i].basecost)
                myPurchase[i].interactable = true;
            else
                myPurchase[i].interactable = false;
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (Coins >= ShopItemSO[btnNo].basecost)
        {
            Coins = (int)(Coins - ShopItemSO[btnNo].basecost);
            CoinUI.text = "Coins:" + Coins.ToString();
            CheckPurcheseable();
        }

    }







    // Update is called once per frame
    void Update()
    {
        CheckPurcheseable();
    }

    public void AddCoins()
    {
        Coins+= 100;
        CoinUI.text = "Coins:" + Coins.ToString();
    }

    public void LoadPanel()

    {
        for (int i = 0; i < ShopItemSO.Length; i++)
        {
            ShopPanels[i].titleText.text = ShopItemSO[i].title;
            ShopPanels[i].descriptionText.text = ShopItemSO[i].description;
            ShopPanels[i].costText.text = "Prezzo:" + ShopItemSO[i].basecost.ToString();
            ShopPanels[i].spriteImage = ShopItemSO[i].spriteIcon;

            Debug.Log(ShopPanels[i].costText);
        }




    }
}
