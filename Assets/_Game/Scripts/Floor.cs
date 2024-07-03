using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Floor : MonoBehaviour
{
    [SerializeField] List<Transform> brickTFs = new List<Transform>();
    [SerializeField] Brick brickPrefab;

    [SerializeField] List<Gate> gates = new List<Gate>();
    [SerializeField] List<Bridge> bridges = new List<Bridge>();
    [SerializeField] int floor;

    private List<int> brickTFsReady = new List<int>();
    private List<int> brickTFsUsed = new List<int>();

    private Dictionary<Color, Dictionary<Brick, int>> colorBricks = new Dictionary<Color, Dictionary<Brick, int>>();

    public void InitFloor()
    {
        for (int i = 0; i < brickTFs.Count; i++)
        {
            brickTFsReady.Add(i);
        }

        for (int i = 0; i < gates.Count; i++)
        {
            gates[i].SetGateFloor(floor);
        }

        for (int i = 0; i < bridges.Count; i++)
        {
            bridges[i].InitBridge();
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
            if (brickTFsReady.Count <= 0)
            {
                break;
            }     

            int temp = Random.Range(0, brickTFsReady.Count);
            int index = brickTFsReady[temp];
            brickTFsReady.RemoveAt(temp);
            brickTFsUsed.Add(index);

            Brick brick = (Brick) SimplePool.Spawn(PoolType.Brick, brickTFs[index].position, Quaternion.identity);

            colorBricks[color].Add(brick, index);

            brick.SetBrickColor(color);
        }
    }

    public void RemoveBrick(Brick brick)
    {
        int index = colorBricks[brick.Color][brick];
        brickTFsReady.Add(index);
        brickTFsUsed.Remove(index);

        colorBricks[brick.Color].Remove(brick);
    }

    public void ClearBrick(Color color)
    {
        if (!colorBricks.ContainsKey(color)) 
        { 
            return; 
        }

        List<Brick> clearBricks = colorBricks[color].Keys.ToList();

        for (int i = 0; i < clearBricks.Count; i++)
        {
            SimplePool.Despawn(clearBricks[i]);
            RemoveBrick(clearBricks[i]);
        }

        colorBricks.Remove(color);
    }
}
