using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<int> gameColors = new List<int>();
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        RandomGameColor();
        SetUpPlayerColor();
    }

    private void RandomGameColor()
    {
        // Chon 5 so trong 0 1 2 3 4 5 6 7 8 9
        for (int i = 0; i < 5; i++)
        {
            int randColor;
            while (true)
            {
                randColor = Random.Range(0, 10);
                bool sameColor = false;
                for (int j = 0; j < gameColors.Count; j++)
                {
                    if (randColor == gameColors[j])
                    {
                        sameColor = true;
                        break;
                    }
                }
                if (!sameColor)
                {
                    break;
                }
            }
            gameColors.Add(randColor);
        }
    }

    private void SetUpPlayerColor()
    {
        player.SetPlayerColor(gameColors[0]);
    }
}
