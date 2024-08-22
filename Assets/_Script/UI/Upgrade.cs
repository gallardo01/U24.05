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
            GameController.instance.playerPrebs.GetComponent<Player>().SetBaseStatAttack(5);
            SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
        }
    }
    public void IncreaseDefend()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        if (gold > upgradeDefPrice)
        {
            GameController.instance.ChangeGold(-upgradeDefPrice);
            GameController.instance.playerPrebs.GetComponent<Player>().SetBaseStatDefend(5);
            SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
        }
    }
    public void IncreaseHp()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        if (gold > upgradeHpPrice)
        {
            GameController.instance.ChangeGold(-upgradeHpPrice);
            GameController.instance.playerPrebs.GetComponent<Player>().SetBaseStatHealth(5);
            SoundManager.instance.PlayOneShot(SoundList.ButtonClick);
        }
    }
}
