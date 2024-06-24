using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private int numberColors;
    private int numberBricksEachColor;

    public int NumberColors => numberColors;
    public int NumberBricksEachColor => numberBricksEachColor;

    void Start()
    {   
        OnInit();
    }

    public void OnInit()
    {
        numberColors = 5;
        numberBricksEachColor = 9;
    }
}
