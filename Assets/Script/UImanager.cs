using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
   public static UImanager instance;
   private int coin;
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
    }

    [SerializeField] TMP_Text coinText;

    public int DisplayCoin()
    {
        coinText.text = coin.ToString();
        return coin;
    }
    public void BuyCharacter()
    {
        coin -= 50;
        DisplayCoin();
    }
    public void EarnCoin()
    {
        coin += 30;
        DisplayCoin();
    }
}
