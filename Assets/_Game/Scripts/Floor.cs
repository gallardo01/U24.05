using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Floor : MonoBehaviour
{
    [SerializeField] List<Transform> brickTFs = new List<Transform>();
    [SerializeField] Brick brickPrefab;

    private List<int> brickTFsIndexReady = new List<int>();
    private List<int> brickTFsIndexUsed = new List<int>();

    private Dictionary<Brick, int> brickTFsIndex = new Dictionary<Brick, int>();

    private Dictionary<Color, Dictionary<Brick, int>> colorBricks = new Dictionary<Color, Dictionary<Brick, int>>();

    public void InitFloor()
    {
        for (int i = 0; i < brickTFs.Count; i++)
        {
            brickTFsIndexReady.Add(i);
        }
    }

    public void GenerateBrick(Color color, int quantity)
    {
        if (!colorBricks.ContainsKey(color))
        {
            colorBricks.Add(color, new Dictionary<Brick, int>());
        }

        for (int i = 0; i < quantity; i++)
        {
            if (brickTFsIndexReady.Count <= 0)
            {
                break;
            }     

            int temp = Random.Range(0, brickTFsIndexReady.Count);
            int index = brickTFsIndexReady[temp];
            brickTFsIndexReady.RemoveAt(temp);
            brickTFsIndexUsed.Add(index);

            //Brick brick = Instantiate(brickPrefab, brickTFs[index]);
            Brick brick = (Brick) SimplePool.Spawn(PoolType.Brick, brickTFs[index].position, Quaternion.identity);
            //brickTFsIndex.Add(brick, index);
            colorBricks[color].Add(brick, index);

            brick.SetBrickColor(color);
        }
    }

    public void RemoveBrick(Brick brick)
    {
        //int index = brickTFsIndex[brick];
        //brickTFsIndexReady.Add(index);
        //brickTFsIndexUsed.Remove(index);

        //brickTFsIndex.Remove(brick);


        int index = colorBricks[brick.color][brick];
        brickTFsIndexReady.Add(index);
        brickTFsIndexUsed.Remove(index);

        colorBricks[brick.color].Remove(brick);

        //StartCoroutine(RegenerateBrick(brick.color, 1, 5f));
    }

    //IEnumerator RegenerateBrick(Color color, int quantity, float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    GenerateBrick(color, quantity);
    //}

    public void ClearBrick()
    {

    }
}
