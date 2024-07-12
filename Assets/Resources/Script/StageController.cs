using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class StageController : MonoBehaviour
{
    [SerializeField] List<Transform> listBricksTransform = new List<Transform>();
    [SerializeField] GameObject brickPrefab;
    public List<Brick> listBricks = new List<Brick>();
    public List<LongBridge> listBridges = new List<LongBridge>();
    public bool isFinalStage = false;
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
    
    public List<Transform> GetPathDestination(Bot bot)
    {
        List<Transform> path = new List<Transform>();
        if (GetTotalBricksInStair(bot.colorIndex) == 0)  // Chua tha? gach - chon 1 cau thang ngau nhien
        {
            int index = Random.Range(0, 3);
            path.Add(listBridges[index].listStairs[0].transform);
            path.Add(listBridges[index].listStairs[listBridges[index].listStairs.Count-1].transform);
        } else // Da~ tha gach roi` -> Chon cau thang co nhieu mau` trung` nhat'
        {
            int index = 0;
            int maxColor = 0;
            for (int i = 0; i < listBridges.Count; i++) // Cau` thang dai`
            {
                int count = listBridges[i].GetTotalBricksColor(bot.colorIndex);
                if (count > maxColor)
                {
                    maxColor = count;
                    index = i;
                }
            }
            path.Add(listBridges[index].listStairs[0].transform);
            path.Add(listBridges[index].listStairs[listBridges[index].listStairs.Count-1].transform);
        }

        if (isFinalStage)
        {
            path.Add(GameObject.FindGameObjectWithTag("Finish").transform);
        }
        return path;
    }


    public bool GetDestinationOfBot(Bot bot)
    {
        // Khi chua tha duoc gach len cau` thang
        if (bot.totalBricks >= 3 && Random.Range(0,10) < 5)
        {
            return true;
        }
        return false;
    }

    private int GetTotalBricksInStair(int color)
    {
        int count = 0;
        for (int i = 0; i < listBridges.Count; i++) // Cau` thang
        {
            count += listBridges[i].GetTotalBricksColor(color);
        }
        return count;
    }

    public Transform GetNearestBricks(Bot bot)
    {
        float distanceMin = float.MaxValue;
        int index = -1;

        for (int i = 0; i < listBricks.Count; i++)
        {
            if (bot.colorIndex == listBricks[i].brickColor)
            {
                float distance = (bot.transform.position - listBricks[i].transform.position).magnitude;
                if (distance < distanceMin)
                {
                    index = i;
                    distanceMin = distance;
                }
            }
        }
        if(index >= 0)
        {
            return listBricks[index].transform;
        }
        return null;
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
            // Brick brick = Instantiate(brickPrefab, listBricksTransform[pos_transform].transform).GetComponent<Brick>();
            Brick brick = EasyObjectPool.Ins.GetObjectFromPool("Brick", listBricksTransform[pos_transform].transform.position,Quaternion.identity).GetComponent<Brick>();
            brick.transform.SetParent(listBricksTransform[pos_transform].transform);
            brick.transform.localPosition = Vector3.zero;
            brick.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            brick.GetComponent<BoxCollider>().enabled = true;
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
        // Brick brick = Instantiate(brickPrefab, listBricksTransform[position].transform).GetComponent<Brick>();
        Brick brick = EasyObjectPool.Ins.GetObjectFromPool("Brick", listBricksTransform[position].transform.position,Quaternion.identity).GetComponent<Brick>();
        brick.transform.SetParent(listBricksTransform[position].transform);
        brick.transform.localPosition = Vector3.zero;
        brick.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        brick.GetComponent<BoxCollider>().enabled = true;
        brick.SetBrickPosition(position);
        brick.SetBrickColor(listColorPlayGame[Random.Range(0, listColorPlayGame.Count)]);
        brick.SetStage(this);
        listBricks.Add(brick);
    }
}
