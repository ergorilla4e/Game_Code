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

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cuoco;
    [SerializeField] private GameObject[] tavoli;

    private Movements movementsPlayer;
    private Dialogo_Cuoco dialogoCuoco;
    private int tavoliAcquistati = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ShopItemSO.Length; i++)
            ShopPanelGO[i].SetActive(true);
        CoinUI.text = "Coins:" + Coins.ToString();
        LoadPanel();
        
        movementsPlayer = player.GetComponent<Movements>();
        dialogoCuoco = cuoco.GetComponent<Dialogo_Cuoco>();
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

            AcquistaOggetto(btnNo);
        }

    }

    public void AcquistaOggetto(int btnNo)
    {
        switch(btnNo)
        {
            case 0:
                movementsPlayer.SetSpeed(16f);
                ShopPanelGO[btnNo].SetActive(false);
                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                break;

            case 7:
                break;

            case 8:
                break;

            case 9:
                break;

            case 10:
                break;

            case 11:
                tavoli[tavoliAcquistati].SetActive(true);
                tavoliAcquistati++;

                if(tavoliAcquistati >= 6)
                {
                    ShopPanelGO[btnNo].SetActive(false);
                }

                break;        
        }
    }

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
        }
    }
}
