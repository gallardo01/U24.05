using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text hero1PriceText;
    [SerializeField] TMP_Text hero2PriceText;
    private int coin;
    private int hero1Price;
    private int hero2Price;
    //public static UImanager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<UImanager>();
    //        }
    //        return instance;
    //    }
    //}
    private void Awake()
    {
        instance = this;
        coin = PlayerPrefs.GetInt("coin", 150);
        hero1Price = 50;
        hero2Price = 30;
    }
    public int DisplayHero1Price()
    {
        hero1PriceText.text = hero1Price.ToString();
        return hero1Price;
    }
    public int DisplayHero2Price()
    {
        hero2PriceText.text = hero2Price.ToString();
        return hero2Price;
    }
    public int DisplayCoin()
    {
        coinText.text = coin.ToString();
        return coin;
    }
    public void BuyCharacter1()
    {
        coin -= 50;
        DisplayCoin();
    }
    public void BuyCharacter2()
    {
        coin -= 30;
        DisplayCoin();
    }
    public void EarnCoin()
    {
        coin += 30;
        DisplayCoin();
    }
}
