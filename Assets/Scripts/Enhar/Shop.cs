using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public ShopManager shopControl;
    public ›tem item;
    public TextMeshProUGUI priceText;

    private void Start()
    {
        priceText.text = item._price.ToString();
    }
    public void Buy()
    {
        if (shopControl.money >= item._price && !item._was›tBought)
        {
            shopControl.money = shopControl.money - item._price;
            shopControl.moneyText.text = shopControl.money.ToString();
            PlayerPrefs.SetInt("_Money", shopControl.money);
            PlayerPrefs.SetInt(item._name, PlayerPrefs.GetInt(item._name,0)+1);
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
        item._was›tBought = true;
    }
    IEnumerator failbuy()
    {
        gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().color = Color.white;
    }

}
[System.Serializable]
public class ›tem
{
    public int _price;
    public bool _was›tBought;
    public int _value;
    public string _name;
}
