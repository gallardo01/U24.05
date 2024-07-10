using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBridge : MonoBehaviour
{
    public List<Stair> listStairs = new List<Stair>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTotalBricksColor(int color)
    {
        int count = 0;
        for(int i = 0; i < listStairs.Count; i++)
        {
            if(listStairs[i].stairColor == color)
            {
                count++;
            }    
        }    
        return count;
    }
}
