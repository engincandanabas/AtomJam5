using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int Money
    {
        get
        {
            return PlayerPrefs.GetInt("Money", 200);
        }
        set
        {
            PlayerPrefs.SetInt("Money", value);
        }
    }
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyMainMenuText;



    public GameObject shopPanel;
    public GameObject shopButton;
    private void Start()
    {
        Money = Money;
        moneyText.text = Money.ToString();
        moneyMainMenuText.text = Money.ToString();
        shopPanel.SetActive(false);
        shopButton.SetActive(true);
    }
    public void ShopPanelOpen()
    {
        shopPanel.SetActive(true);
        shopButton.SetActive(false);
    }

    public void ShopPanelClose()
    {
        shopPanel.SetActive(false);
        shopButton.SetActive(true);
    }
}
