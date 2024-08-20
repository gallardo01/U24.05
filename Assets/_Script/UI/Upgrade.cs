using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public TMPro.TextMeshProUGUI attack;
    public TMPro.TextMeshProUGUI health;
    public TMPro.TextMeshProUGUI defend;
    private int upgradeAtk;
    private int upgradeDef;
    private int upgradeHp;

    void Start()
    {
        upgradeAtk = 40;
        upgradeDef = 80;
        upgradeHp = 25;
    }

    public void IncreaseAttack()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        if (gold >  upgradeAtk)
        {
            GameController.instance.ChangeGold(-upgradeAtk);
            GameController.instance.playerPrebs.GetComponent<Player>().SetBaseStatAttack(5);
        }
    }
    public void IncreaseDefend()
    {
        int gold = PlayerPrefs.GetInt("Gold");
        if (gold > upgradeDef)
        {
            GameController.instance.ChangeGold(-upgradeDef);
            GameController.instance.playerPrebs.GetComponent<Player>().SetBaseStatDefend(5);
        }
    }
    //public void IncreaseHp()
    //{
    //    int gold = PlayerPrefs.GetInt("Gold");
    //    if (gold > upgradeHp)
    //    {
    //        GameController.instance.ChangeGold(-upgradeHp);
    //        GameController.instance.playerPrebs.GetComponent<Player>().ChangeStatOfPlayer(0, 0);
    //    }
    //}
}
