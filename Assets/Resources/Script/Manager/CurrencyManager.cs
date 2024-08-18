using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : Singleton<CurrencyManager>, IGameStateListener
{
    [SerializeField] TextMeshProUGUI[] currencyText;
    [SerializeField] TextMeshProUGUI currencyClamb;

    private const string CurrencyKey = "Currency";
    public int CurrentCurrency { get; private set; }
    [SerializeField] private Button clambButton;

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
        ES3.Save<int>(CurrencyKey, CurrentCurrency);      
    }

    private void LoadCurrency()
    {
        CurrentCurrency = ES3.Load<int>(CurrencyKey, 0); // Default to 0 if not found
        DisplayCurrency();
    }

    public void ResetCurrency()
    {
        CurrentCurrency = 0;
        ES3.Save<int>(CurrencyKey, CurrentCurrency);
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

    private void Clamp(int point)
    {
        clambButton.interactable = false;
        ClambAnim();
        AddCurrency(point);
    }

    private void ClambAnim()
    {

    }

    private void ClambCurrencyDisplay(int point)
    {
        currencyClamb.text = point.ToString();
        SoundManager.ButtonClick();
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                LoadCurrency();
                clambButton.onClick.AddListener(() => Clamp(GameManager.RoundPoint));
                break;
            case GameState.GAMEOVER:
                ClambCurrencyDisplay(GameManager.RoundPoint);
                break;
        }
    }
}
