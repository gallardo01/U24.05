using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SoundManager;

public class Upgrade : MonoBehaviour
{
    public TMPro.TextMeshProUGUI attack;
    public TMPro.TextMeshProUGUI health;
    public TMPro.TextMeshProUGUI defend;
    private int upgradeAtkPrice;
    private int upgradeDefPrice;
    private int upgradeHpPrice;
    private InitSkin initSkin;

    void Start()
    {
        upgradeAtkPrice = 40;
        upgradeDefPrice = 80;
        upgradeHpPrice = 25;
    }

    public void IncreaseAttack()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        if (gold >  upgradeAtkPrice)
        {
            GameController.instance.ChangeGold(-upgradeAtkPrice);
            GameController.instance.playerPrebs.SetBaseStatAttack(1);
            SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
            UpdateStats();
        }
    }
    public void IncreaseDefend()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        if (gold > upgradeDefPrice)
        {
            GameController.instance.ChangeGold(-upgradeDefPrice);
            GameController.instance.playerPrebs.SetBaseStatDefend(1);
            SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
            UpdateStats();
        }
    }
    public void IncreaseHp()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        if (gold > upgradeHpPrice)
        {
            GameController.instance.ChangeGold(-upgradeHpPrice);
            GameController.instance.playerPrebs.SetBaseStatHealth(5);
            SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
            UpdateStats();
        }
    }

    public void DisplayStat()
    {
        GameController.instance.playerPrebs.stats.DisplayStats();
    }

    public void UpdateStats()
    {
        initSkin = GameController.instance.playerPrebs.initSkin;
        initSkin.AddStatToCharacter();
        DisplayStat();
    }
}
