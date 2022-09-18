using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public ShopManager shopControl;
    public Ýtem item;
    public TextMeshProUGUI priceText;

    private void Start()
    {
        priceText.text = item._price.ToString();
        if(!PlayerPrefs.HasKey("Mouse"))
        {
            // oyuncu oyuna ilk defa girdi
            PlayerPrefs.SetInt("Mouse", 3);
        }
    }
    public void Buy()
    {
        if (shopControl.Money >= item._price)
        {
            shopControl.Money -= item._price;
            shopControl.moneyText.text = shopControl.Money.ToString();
            shopControl.moneyMainMenuText.text = shopControl.Money.ToString();

            PlayerPrefs.SetInt(item._name, PlayerPrefs.GetInt(item._name,0)+1);
            HeroesPlaceManager.instance.InitializeVariables();
            StartCoroutine(buy());
        }
        else
        {
            StartCoroutine(failbuy());
        }
    }
    IEnumerator buy()
    {
        gameObject.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().color = Color.white;
    }
    IEnumerator failbuy()
    {
        gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().color = Color.white;
    }

}
[System.Serializable]
public class Ýtem
{
    public int _price;
    public int _value;
    public string _name;
}
