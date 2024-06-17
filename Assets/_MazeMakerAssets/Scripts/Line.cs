using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public bool gotBrick = false;
    public bool GotBrick => gotBrick;

    public void FillBrick()
    {
        gotBrick = true;
    }

    void Update()
    {
        
    }
}
