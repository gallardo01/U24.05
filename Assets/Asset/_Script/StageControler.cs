using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageControler : MonoBehaviour
{
    [SerializeField] List<Transform> transformBricks = new List<Transform>();
    [SerializeField] GameObject brickPrefab;
    int countBrickPlayer0 = 1;
    int countBrickPlayer1 = 1;
    int countBrickPlayer2 = 1;

    List<GameObject> bricks = new List<GameObject>();

    [SerializeField]  List<GameObject> getColorPlayers = new List<GameObject>();

    private void Start()
    {
        OnInit();
        CreatColorPlayer();
    }

    private void OnInit()
    {
        for (int i = 0; i < transformBricks.Count; i++)
        {
            Quaternion rotationBrick = Quaternion.Euler(0,90,0);
            GameObject brick = Instantiate(brickPrefab, transformBricks[i].position, rotationBrick);
            CreatColorBrick(brick);
            brick.transform.SetParent(transformBricks[i]); 
            bricks.Add(brick);
        }
    }
    //ve nha lam tiep
    private void CreatColorPlayer()
    {
        for (int i = 0; i < getColorPlayers.Count; i++)
        {
            while (true)
            {
                int colorIndex = Random.Range(0, ColorController.Instance.materials.Count);
                if (colorIndex  )
                {
                    getColorPlayers[i].GetComponent<Player>().SetPlayerColor(colorIndex);
                }
            }
            
        }  
    }
    private void CreatColorBrick(GameObject brick)
    {

        while (true)
        {
            int random = Random.Range(0, getColorPlayers.Count);
            if (random == 0)
            {
                countBrickPlayer0++;
                if (countBrickPlayer0 > 18)
                {
                    continue;
                }
            }
            else if (random == 1)
            {
                countBrickPlayer1++;
                if (countBrickPlayer1 > 18)
                {
                    continue;
                }
            }
            else if (random == 2)
            {
                countBrickPlayer2++;
                if (countBrickPlayer2 > 18)
                {
                    continue;
                }
            }
            brick.GetComponent<Brick>().SetBrickColor(getColorPlayers[random].colorIndex);
            break;
        }     
    }
}
