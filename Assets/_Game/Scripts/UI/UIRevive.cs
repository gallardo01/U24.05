using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRevive : UICanvas
{
    [SerializeField] Button btnRevive, btnQuit;
    [SerializeField] TextMeshProUGUI textCountdown;

    [SerializeField] Transform ImageCountdown;

    int countdown;

    private void Awake()
    {
        btnRevive.onClick.AddListener(() =>
        {
            CloseDirectly();
            LevelManager.Ins.RevivePlayer();
            UIManager.Ins.OpenUI<UIGameplay>();
        });

        btnQuit.onClick.AddListener(() =>
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIDefeat>();
        });
    }

    private void Update()
    {
        ImageCountdown.Rotate(0f, 0f, -360f * Time.deltaTime);
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeGameState(GameState.Revive);
        countdown = 5;
        Countdown();
    }

    IEnumerator IECountdown()
    {
        yield return new WaitForSeconds(1f);
        Countdown();
    }

    void Countdown()
    {
        if (countdown < 0)
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIDefeat>();
        }
        else
        {
            textCountdown.text = countdown.ToString();
            countdown--;
            StartCoroutine(IECountdown());
        }
    }
}
