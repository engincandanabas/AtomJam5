using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public int Money
    {
        get
        {
            return PlayerPrefs.GetInt("Money", 200);
        }
        set
        {
            PlayerPrefs.SetInt("Money", value);
            moneyMainMenuText.text=Money.ToString();
            moneyText.text=Money.ToString();
        }
    }
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyMainMenuText;



    public GameObject shopPanel;
    public GameObject shopButton;
    private void Awake()
    {
        instance = this;
    }
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
