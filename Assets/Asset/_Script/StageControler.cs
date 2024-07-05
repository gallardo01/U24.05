using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class StageControler : MonoBehaviour
{
    [SerializeField] public List<Transform> transformBricks = new List<Transform>();
    [SerializeField] GameObject brickPrefab;

    public List<Brick> bricksList = new List<Brick>();

    public List<GameObject> getColorPlayers = new List<GameObject>();

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        for (int i = 0; i < transformBricks.Count; i++)
        {
            Quaternion rotationBrick = Quaternion.Euler(0,90,0);
            GameObject brick = Instantiate(brickPrefab, transformBricks[i].position, rotationBrick);
            GameController.Instance.CreatColorBrick(brick);
            brick.GetComponent<Brick>().SetBrickPosition(i);
            brick.transform.SetParent(transformBricks[i]);
            brick.GetComponent<Brick>().SetBrickPosition(i);
            brick.GetComponent<Brick>().SetStage(this);
            bricksList.Add(brick.GetComponent<Brick>());
        }
    }

    public void CharacterStartGame(GameObject gameObject)
    {
        if (!getColorPlayers.Contains(gameObject))
        {
            getColorPlayers.Add(gameObject);
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
        Brick brick = Instantiate(brickPrefab, transformBricks[pos].position, rotationBrick).GetComponent<Brick>();
        bricksList.Add(brick);
        brick.SetBrickPosition(pos);
        brick.SetStage(this);

        // Tao mau random cho brick moi va dat vao vi tri tuong ung
        int random = Random.Range(0, getColorPlayers.Count);
        int colorBrick = getColorPlayers[random].GetComponent<Character>().colorIndex;
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
            if (bot.colorIndex == bricksList[i].brickColor)
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
            Debug.Log(index);
            return bricksList[index].transform.position;
        }
        return Vector3.zero;     
    }
}
