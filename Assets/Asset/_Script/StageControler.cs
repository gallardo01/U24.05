using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageControler : Singleton<StageControler>
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
    }

    private void OnInit()
    {
        CreatColorPlayer();
        for (int i = 0; i < transformBricks.Count; i++)
        {
            Quaternion rotationBrick = Quaternion.Euler(0,90,0);
            GameObject brick = Instantiate(brickPrefab, transformBricks[i].position, rotationBrick);
            CreatColorBrick(brick);
            brick.GetComponent<Brick>().SetBrickPosition(i);
            brick.transform.SetParent(transformBricks[i]);
            bricks.Add(brick);
        }
    }
    public void CreatBrickRepeat(int pos)
    {
        StartCoroutine(CreatBrickRepeatCoroutine(pos));
    }

    IEnumerator CreatBrickRepeatCoroutine(int pos)
    {
        yield return new WaitForSeconds(2f);
        Quaternion rotationBrick = Quaternion.Euler(0, 90, 0);
        Brick brick = Instantiate(brickPrefab, transformBricks[pos].position, rotationBrick).GetComponent<Brick>();
        brick.SetBrickPosition(pos);
        int random = Random.Range(0, getColorPlayers.Count);
        int colorBrick = getColorPlayers[random].GetComponent<Character>().colorIndex;
        brick.SetBrickColor(colorBrick);
        brick.transform.SetParent(transformBricks[pos]);
        bricks.Add(brick.gameObject);
    }

    private void CreatColorPlayer()
    {
        int[] colorExists = new int[getColorPlayers.Count];
        for (int i = 0; i < getColorPlayers.Count; i++)
        {
            int colorIndex;
            do
            {
                colorIndex = Random.Range(0, ColorController.Instance.materials.Count);
                getColorPlayers[i].GetComponent<Character>().SetPlayerColor(colorIndex);
            } while (Array.Exists(colorExists, num => num == colorIndex));
            colorExists[i] = colorIndex;
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
            int colorBrick = getColorPlayers[random].GetComponent<Character>().colorIndex;
            brick.GetComponent<Brick>().SetBrickColor(colorBrick);
            break;
        }     
    }

}
