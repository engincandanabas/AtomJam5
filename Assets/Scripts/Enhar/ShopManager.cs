using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;

    public GameObject shopPanel;
    public GameObject shopButton;

    private void Start()
    {
        moneyText.text = money.ToString();
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
