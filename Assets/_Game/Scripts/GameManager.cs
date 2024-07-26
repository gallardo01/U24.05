using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        LevelManager.Ins.OnLoadLevel(0);
    }
}
