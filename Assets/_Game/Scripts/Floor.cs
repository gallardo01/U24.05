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



    void Start()
    {
        for (int i = 0; i < brickTFs.Count; i++)
        {
            brickTFsIndexReady.Add(i);
        }

        for (int i = 0; i < Constants.QUANTITY_COLOR_GENERATE; i++)
        {
            GenerateBricks((Color)ColorController.Ins.colorsIndexUsed[i], Constants.QUANTITY_BRICK_PER_COLOR);
        }
    }

    public void GenerateBricks(Color color, int quantity)
    {
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
            brickTFsIndex.Add(brick, index);

            brick.SetBrickColor(color);
        }
    }

    public void RemoveBrick(Brick brick)
    {
        int index = brickTFsIndex[brick];
        brickTFsIndexReady.Add(index);
        brickTFsIndexUsed.Remove(index);

        brickTFsIndex.Remove(brick);
    }
}
