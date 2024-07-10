using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] List<Transform> listBricksTransform = new List<Transform>();
    [SerializeField] GameObject brickPrefab;
    public List<Brick> listBricks = new List<Brick>();
    public List<LongBridge> listBridges = new List<LongBridge>();

    private List<int> listColorPlayGame = new List<int>();
    private List<int> listBricksInMap = new List<int>();

    public bool isFinalStage = false; 

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

    public Transform GetNearestBricks(Bot bot)
    {
        float minDistance = float.MaxValue;
        int index = -1;

        for (int i = 0; i < listBricks.Count; i++)
        {
            if (bot.colorIndex == listBricks[i].brickColor)
            {
                float distance = (bot.transform.position - listBricks[i].transform.position).magnitude;

                if (distance < minDistance)
                {
                    index = i;
                    minDistance = distance;
                }
            }
        }

        if (index >= 0)
        {
            return listBricks[index].transform;
        }

        return null;
    }

    public bool GetDestinationOfBot(Bot bot)
    {
        if (bot.totalBricks >= 3 && Random.Range(0, 10) < 5)
        {
            return true;
        }
        return false;
    }

    //Get total brick
    private int GetTotalBricksInStair(int color)
    {
        int count = 0;
        for (int i = 0; i < listBridges.Count; i++)
        {
            count += listBridges[i].GetTotalBricksColor(color);
        }
        return count;
    }

    public List<Transform> GetPathDestination(Bot bot)
    {
        List<Transform> path = new List<Transform>();
        if(GetTotalBricksInStair(bot.colorIndex) == 0) //not drop any brick yet, select random bridge
        {
            int index = Random.Range(0, 3);
            path.Add(listBridges[index].listStairs[0].transform);
            path.Add(listBridges[index].listStairs[listBridges[index].listStairs.Count-1].transform);
        } 
        else //select bridge with maximum of number of dropped brick same bot's color 
        {
            int index = 0;
            int maxColor = 0;
            for (int i = 0; i < listBridges.Count; i++)
            {
                int count = listBridges[i].GetTotalBricksColor(bot.colorIndex);

                if(count > maxColor)
                {
                    maxColor = count;
                    index = i;
                }    
            }
            path.Add(listBridges[index].listStairs[0].transform);
            path.Add(listBridges[index].listStairs[listBridges[index].listStairs.Count - 1].transform);
        }    
        if(isFinalStage)
        {
            path.Add(GameController.Ins.finishPoints);
        }    
        return path;
    }
}
