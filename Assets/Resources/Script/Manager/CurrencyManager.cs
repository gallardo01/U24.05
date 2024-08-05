using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [SerializeField] TextMeshProUGUI[] currencyText;

    private const string CurrencyKey = "Currency";
    public int CurrentCurrency { get; private set; }

    private void Start()
    {
        LoadCurrency();
    }

    public void AddCurrency(int amount)
    {
        CurrentCurrency += amount;
        SaveCurrency();
        DisplayCurrency();
    }

    public bool SpendCurrency(int amount)
    {
        if (CurrentCurrency >= amount)
        {
            CurrentCurrency -= amount;
            SaveCurrency();
            DisplayCurrency();
            return true;
        }
        return false;
    }

    private void SaveCurrency()
    {
        PlayerPrefs.SetInt(CurrencyKey, CurrentCurrency);
    }

    private void LoadCurrency()
    {
        CurrentCurrency = PlayerPrefs.GetInt(CurrencyKey, 0); // Default to 0 if not found
        DisplayCurrency();
    }

    public void ResetCurrency()
    {
        CurrentCurrency = 0;
        SaveCurrency();
        DisplayCurrency();
    }

    private void DisplayCurrency()
    {
        for(int i = 0; i < currencyText.Length; i++)
        {
            currencyText[i].text = CurrentCurrency.ToString();
        }
    }
}
