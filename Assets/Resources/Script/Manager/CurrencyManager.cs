using DG.Tweening;
using Lean.Pool;
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

    [SerializeField] Button clambButton;
    [SerializeField] RectTransform coinPrefab;
    [SerializeField] Transform coinsParent;
    [SerializeField] RectTransform startPosition;
    [SerializeField] RectTransform endPosition;

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
    }

    private void ClambAnim()
    {
        StartCoroutine(ClambAnimSequence());
    }

    IEnumerator ClambAnimSequence()
    {
        int endCurrency = GameManager.Instance.RoundPoint;
        DOTween.To(() => endCurrency, x => endCurrency = x, 0, 1.5f)
               .OnUpdate(() => { currencyClamb.text = endCurrency.ToString(); })
               .OnComplete(() => AddCurrency(GameManager.Instance.RoundPoint));
               
        for (int i = 0; i < 10; i++)
        {
            RectTransform coin = LeanPool.Spawn(coinPrefab, coinsParent);
            coin.anchoredPosition = startPosition.anchoredPosition;
            coin.localScale = Vector3.zero;
            coin.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                coin.DOAnchorPos(endPosition.anchoredPosition, 1f).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    LeanPool.Despawn(coin);
                    coin.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad);
                });
            });
            yield return new WaitForSeconds(0.1f);
        }
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
                break;
            case GameState.GAMEOVER:
                clambButton.onClick.AddListener(() => Clamp(GameManager.Instance.RoundPoint));
                ClambCurrencyDisplay(GameManager.Instance.RoundPoint);
                break;
        }
    }
}
