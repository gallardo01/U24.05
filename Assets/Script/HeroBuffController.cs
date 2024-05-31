using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBuffController : HeroBase
{
    void Start()
    {
        if (0 <= currentNode - 1)
        {
            Controller.Ins.heroNodes[currentNode - 1].AddBuff(BuffType.DoubleDamage, 1);
        }

        if (0 <= currentNode - 4)
        {
            Controller.Ins.heroNodes[currentNode - 4].AddBuff(BuffType.DoubleDamage, 1);
        }

        if (currentNode + 1 <= Controller.Ins.heroNodes.Count)
        {
            Controller.Ins.heroNodes[currentNode + 1].AddBuff(BuffType.DoubleDamage, 1);
        }

        if (currentNode +4 <= Controller.Ins.heroNodes.Count)
        {
            Controller.Ins.heroNodes[currentNode + 4].AddBuff(BuffType.DoubleDamage, 1);
        }          
    }
}
