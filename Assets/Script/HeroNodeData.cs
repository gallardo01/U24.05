using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroNodeData : MonoBehaviour
{
    public Transform trans;

    public Dictionary<BuffType, int> Buffs = new Dictionary<BuffType, int>();

    public void AddBuff(BuffType buffType, int quantity)
    {
        if (Buffs.ContainsKey(buffType)) 
        {
            Buffs[buffType] += quantity;
        }
        else
        {
            Buffs.Add(buffType, quantity);
        }
    }
}
