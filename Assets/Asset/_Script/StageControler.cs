using System;
using System.Collections;
using System.Collections.Generic;
using MarchingBytes;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class StageControler : MonoBehaviour
{
    [SerializeField] public List<Transform> transformBricks = new List<Transform>();
    [SerializeField] GameObject brickPrefab;
    [SerializeField] public List<Transform> finishStage = new List<Transform>();

    List<int> numberPos = new List<int>();
    public List<Brick> bricksList = new List<Brick>();
    public List<Character> colorPlayers = new List<Character>();

    private void Start()
    {    
        for (int i = 0; i < transformBricks.Count; i++)
        {
            numberPos.Add(i);
        }
    }

    public void OnInit(Character player)
    {
        int numberPlayer = GameController.Instance.playerList.Count;
        for (int i = 0; i < (transformBricks.Count / numberPlayer); i++)
        {
            int random = Random.Range(0, numberPos.Count);
            
            Brick brick = EasyObjectPool.Instance.GetObjectFromPool("Brick", transformBricks[numberPos[random]].position, Quaternion.Euler(0, 90, 0)).GetComponent<Brick>();
            GameController.Instance.CreatColorBrick(brick,player);

            brick.transform.SetParent(transformBricks[numberPos[random]]);
            brick.SetBrickPosition(numberPos[random]);
            numberPos.Remove(numberPos[random]);
            brick.SetStage(this);
            brick.GetComponent<BoxCollider>().enabled = true;
            bricksList.Add(brick);
        }
    }

    public void CharacterStartGame(Character player)
    {
        if (!colorPlayers.Contains(player))
        {
            colorPlayers.Add(player);
            OnInit(player);
        }
    }

    public void CreatBrickRepeat(int pos)
    {
        StartCoroutine(CreatBrickRepeatCoroutine(pos));
    }

    IEnumerator CreatBrickRepeatCoroutine(int pos)
    {
        yield return new WaitForSeconds(2f);

        //Tao brick va add vao list
        Quaternion rotationBrick = Quaternion.Euler(0, 90, 0);
        Brick brick = EasyObjectPool.Instance.GetObjectFromPool("Brick", transformBricks[pos].position, rotationBrick).GetComponent<Brick>();
        brick.SetBrickPosition(pos);
        brick.SetStage(this);
        brick.GetComponent<BoxCollider>().enabled = true;

        // Tao mau random cho brick moi va dat vao vi tri tuong ung
        int random = Random.Range(0, colorPlayers.Count);
        int colorBrick = colorPlayers[random].GetComponent<Character>().colorIndex;
        brick.SetBrickColor(colorBrick);
        brick.transform.SetParent(transformBricks[pos]);
        bricksList.Add(brick);
    }

    public Vector3 GetNearestBrick(Bot bot)
    {
        float distanceMin = float.MaxValue;
        int index = -1;
        for (int i = 0; i < bricksList.Count; i++)
        {
            float distance = Vector3.Distance(bricksList[i].transform.position, bot.transform.position);
            if (bricksList[i].brickColor == bot.colorIndex)
            {
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    index = i;
                }
            }
        }
        if (index >=0)
        {
            return bricksList[index].transform.position;
        }
        return Vector3.zero;     
    }
    public Transform SetRandomFinishPoint()
    {
        int random = Random.Range(0, finishStage.Count);
        return finishStage[random];
    }
}
