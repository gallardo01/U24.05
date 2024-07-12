using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBridge : MonoBehaviour
{
    public List<Stair> listStairs = new List<Stair>();

    public int GetTotalBricksColor(int color)
    {
        int count = 0;
        for (int i = 0; i < listStairs.Count; i++)
        {
            if (color == listStairs[i].stairColor)
            {
                count++;
            }
        }
        return count;
    }
}
