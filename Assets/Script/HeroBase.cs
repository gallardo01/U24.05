using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour
{
    public int currentNode;

    public Dictionary<BuffType, int> Buffs = new Dictionary<BuffType, int>();

    public void GetBuffs()
    {
        Buffs = Controller.Ins.heroNodes[currentNode].Buffs; 
    }
}
