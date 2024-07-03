using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageControler : Singleton<StageControler>
{
    [SerializeField] public List<Transform> transformBricksStagw1 = new List<Transform>();
    [SerializeField] public List<Transform> transformBricksStage2 = new List<Transform>();

    [SerializeField] GameObject brickPrefab;

    public List<GameObject> bricksListStage1 = new List<GameObject>();
    public List<GameObject> bricksListStage2 = new List<GameObject>();

    [SerializeField] List<GameObject> getColorPlayersStage1 = new List<GameObject>();
    [SerializeField] public List<GameObject> getColorPlayersStage2 = new List<GameObject>();

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        CreatColorPlayer();
        for (int i = 0; i < transformBricksStagw1.Count; i++)
        {
            Quaternion rotationBrick = Quaternion.Euler(0,90,0);
            GameObject brick = Instantiate(brickPrefab, transformBricksStagw1[i].position, rotationBrick);
            CreatColorBrick(brick,getColorPlayersStage1);
            brick.GetComponent<Brick>().SetBrickPosition(i,transformBricksStagw1);
            brick.transform.SetParent(transformBricksStagw1[i]);
            bricksListStage1.Add(brick);
        }
    }

    public void CreatBrickStage2()
    {
        for (int i = 0; i < transformBricksStage2.Count; i++)
        {
            Quaternion rotationBrick = Quaternion.Euler(0, 90, 0);
            GameObject brick = Instantiate(brickPrefab, transformBricksStage2[i].position, rotationBrick);
            CreatColorBrick(brick, getColorPlayersStage2);
            brick.GetComponent<Brick>().SetBrickPosition(i,transformBricksStage2);
            brick.transform.SetParent(transformBricksStage2[i]);
            bricksListStage2.Add(brick);
        }
    }
    public void CreatBrickRepeat(int pos, List<GameObject> bricksList, List<Transform> transformBricks)
    {
        StartCoroutine(CreatBrickRepeatCoroutine(pos, bricksList,transformBricks));
    }

    IEnumerator CreatBrickRepeatCoroutine(int pos, List<GameObject> bricksList, List<Transform> transformBricks)
    {
        yield return new WaitForSeconds(2f);

        //Tao brick va add vao list
        Quaternion rotationBrick = Quaternion.Euler(0, 90, 0);
        Brick brick = Instantiate(brickPrefab, transformBricks[pos].position, rotationBrick).GetComponent<Brick>();
        brick.SetBrickPosition(pos, transformBricks);
        bricksList.Add(brick.gameObject);

        // Tao mau random cho brick moi va dat vao vi tri tuong ung
        int random = Random.Range(0, getColorPlayersStage1.Count);
        int colorBrick = getColorPlayersStage1[random].GetComponent<Character>().colorIndex;
        brick.SetBrickColor(colorBrick);
        brick.transform.SetParent(transformBricks[pos]);
        bricksList.Add(brick.gameObject);
    }

    private void CreatColorPlayer()
    {
        int[] colorExists = new int[getColorPlayersStage1.Count];
        for (int i = 0; i < getColorPlayersStage1.Count; i++)
        {
            int colorIndex;
            do
            {
                colorIndex = Random.Range(0, ColorController.Instance.materials.Count);
                getColorPlayersStage1[i].GetComponent<Character>().SetPlayerColor(colorIndex);
            } while (Array.Exists(colorExists, num => num == colorIndex));
            colorExists[i] = colorIndex;
        }  
    }
    private void CreatColorBrick(GameObject brick,List<GameObject> getcolorPlayers)
    {
        List<int> countColorBrick = new List<int>();
        for (int i = 0; i < getcolorPlayers.Count; i++)
        {
            countColorBrick.Add(0);
        }
        while (true)
        {
            int random = Random.Range(0, getcolorPlayers.Count);
            for (int i = 0; i < getcolorPlayers.Count; i++)
            {
                if (random == i)
                {
                    countColorBrick[i]++;
                    if (countColorBrick[i]>18)
                    {
                        continue;
                    }
                }
            }        
            int colorBrick = getcolorPlayers[random].GetComponent<Character>().colorIndex;
            brick.GetComponent<Brick>().SetBrickColor(colorBrick);
            break;
        }     
    }

}
