using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : Singleton<StageController>
{
    [SerializeField] List<Transform> listBricksTransform = new List<Transform>();
    [SerializeField] GameObject brickPrefab;
    private List<Brick> listBricks = new List<Brick>();
    private List<int> listColorPlayGame = new List<int>();

    private List<int> listBricksInMap = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < listBricksTransform.Count; i++)
        {
            listBricksInMap.Add(i);
        }
    }
    public void CharacterStartGame(int color)
    {
        if (!listColorPlayGame.Contains(color))
        {
            listColorPlayGame.Add(color);
            // Sinh ra 10 gach ngau nhien
            CreateNewBricksForCharacter(color);
        }
    }
    private void CreateNewBricksForCharacter(int color)
    {
        // Sinh ra 10 vien gach trong nhung diem con` lai chua co gach?
        for (int i = 0; i < 10; i++)
        {
            int pos = Random.Range(0, listBricksInMap.Count);  
            int pos_transform = listBricksInMap[pos]; 
            listBricksInMap.Remove(pos_transform);

            // Sinh ra gach 3  4
            Brick brick = Instantiate(brickPrefab, listBricksTransform[pos_transform].transform).GetComponent<Brick>();
            brick.transform.localPosition = Vector3.zero;
            brick.SetBrickPosition(pos_transform);
            brick.SetBrickColor(color);
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
        brick.SetBrickColor(0);
        listBricks.Add(brick);
    }
}
