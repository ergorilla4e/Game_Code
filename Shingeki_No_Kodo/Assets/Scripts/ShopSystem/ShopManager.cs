using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics.CodeAnalysis;

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
    [SerializeField] private HumorBar humor;

    private Player movementsPlayer;
    private Cuoco dialogoCuoco;
    private int tavoliAcquistati = 0;

    private void Awake()
    {
        movementsPlayer = player.GetComponent<Player>();
        dialogoCuoco = cuoco.GetComponent<Cuoco>();
    }

    private void Start()
    {
        //Funzione che faceva comparire solo i template che avevano al loro interno degli ScriptableObject
        //for (int i = 0; i < ShopItemSO.Length; i++)
        //    ShopPanelGO[i].SetActive(true);

        CoinUI.text = "Coins:" + Coins.ToString();
        LoadPanel();
        
        this.Coins = 15;
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

            AcquistaOggetto(btnNo);
        }
    }

    public void inserisciInCuoco(int indirizzo)
    {
        dialogoCuoco.arrayOfBubbleTea[indirizzo-1]++;
        ShopPanels[indirizzo].possedutiText.text = "Posseduti:" + dialogoCuoco.arrayOfBubbleTea[indirizzo - 1];
        dialogoCuoco.nuemeroBBTPosseduti[indirizzo-1].text = "" + dialogoCuoco.arrayOfBubbleTea[indirizzo - 1];
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
                inserisciInCuoco(btnNo);
                break;

            case 2:
                inserisciInCuoco(btnNo);
                break;

            case 3:
                inserisciInCuoco(btnNo);
                break;

            case 4:
                inserisciInCuoco(btnNo);
                break;

            case 5:
                inserisciInCuoco(btnNo);
                break;

            case 6:
                inserisciInCuoco(btnNo);
                break;

            case 7:
                inserisciInCuoco(btnNo);
                break;

            case 8:
                inserisciInCuoco(btnNo);
                break;

            case 9:
                inserisciInCuoco(btnNo);
                break;

            case 10:
                inserisciInCuoco(btnNo);
                break;

            case 11:
                tavoli[tavoliAcquistati].SetActive(true);
                tavoliAcquistati++;

                humor.addHumor(20);
                humor.UpdateGraphics();

                if(tavoliAcquistati >= 6)
                {
                    ShopPanelGO[btnNo].SetActive(false);
                }
                break;

            case 12:
                dialogoCuoco.velocitaCuoco--;

                ShopPanels[btnNo].possedutiText.text = "Velocita':" + dialogoCuoco.velocitaCuoco + " secondi";

                if (dialogoCuoco.velocitaCuoco <= 0)
                {
                    ShopPanelGO[btnNo].SetActive(false);
                }
            
                break;
        }
    }

    void Update()
    {
        CheckPurcheseable();
        CoinUI.text = "Coins:" + Coins.ToString();
    }

    public void AddCoins()
    {
        Coins+= 100;
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

    public int GetTavoliAcquistati()
    {
        return tavoliAcquistati;
    }
}
