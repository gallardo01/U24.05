using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StageController : MonoBehaviour
{
    [SerializeField] List<Transform> listBricksTransform = new List<Transform>();
    [SerializeField] GameObject brickPrefab;
    private List<Brick> listBricks = new List<Brick>();
    private List<int> listColorPlayGame = new List<int>();
    private List<int> listBrickInMap = new List<int>();
    // Start Game - Pick mau`


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < listBricksTransform.Count; i++) 
        {
            listBrickInMap.Add(i);
        }
    }

    public void CharacterStartGame(int color)
    {
        if (!listColorPlayGame.Contains(color)) 
        { 
            listColorPlayGame.Add(color);

            CreateNewBricksForCharacter(color);
        }
    }

    private void CreateNewBricksForCharacter(int color)
    {
        for (int i = 0; i < 10; i++)
        {
            int pos = Random.Range(0, listBrickInMap.Count);
            int pos_transform = listBrickInMap[pos];
            listBrickInMap.Remove(pos_transform);
            Brick brick = Instantiate(brickPrefab, listBricksTransform[pos_transform].transform).GetComponent<Brick>();
            brick.transform.localPosition = Vector3.zero;
            brick.SetBrickPosition(pos_transform);
            brick.SetBrickColor(color);
            brick.SetStage(this);
            listBricks.Add(brick);
        }
    }

    public void CreateNewBrick(int position)
    {
        StartCoroutine(CreateNewBrickAfterDelayTime(position));
    }

    IEnumerator CreateNewBrickAfterDelayTime(int position)
    {
        yield return new WaitForSeconds(5f);
        Brick brick = Instantiate(brickPrefab, listBricksTransform[position].transform).GetComponent<Brick>();
        brick.transform.localPosition = Vector3.zero;
        brick.SetBrickPosition(position);
        brick.SetBrickColor(listColorPlayGame[Random.Range(0, listColorPlayGame.Count)]);
        brick.SetStage(this);
        listBricks.Add(brick);
    }
}
