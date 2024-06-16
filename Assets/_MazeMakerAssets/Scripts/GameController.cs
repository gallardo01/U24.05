using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public void EndLevel()
    {
        Time.timeScale = 0;
        Debug.Log("EndLevel");
    }

}
