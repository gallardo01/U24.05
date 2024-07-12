using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Floor : MonoBehaviour
{
    [SerializeField] List<Transform> brickTFs = new List<Transform>();
    [SerializeField] Brick brickPrefab;

    [SerializeField] int floor;
    [SerializeField] List<Gate> gates = new List<Gate>();
    [SerializeField] List<Bridge> bridges = new List<Bridge>();

    private List<int> brickTFsReady = new List<int>();
    private List<int> brickTFsUsed = new List<int>();

    private Dictionary<GameColor, Dictionary<Brick, int>> colorBricks = new Dictionary<GameColor, Dictionary<Brick, int>>();

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

    public void GenerateBrick(GameColor color, int quantity)
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

    public void ClearBrick(GameColor color)
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

    public void ClearAllBrick()
    {
        List<GameColor> colors = colorBricks.Keys.ToList();

        for (int i = 0; i < colors.Count; i++)
        {
            ClearBrick(colors[i]);
        }
    }

    public List<Vector3> GetListBrickPos(GameColor color)
    {
        if (!colorBricks.ContainsKey(color))
        {
            return null;
        }

        List<int> brickPosIndexs = colorBricks[color].Values.ToList();
        List<Vector3> brickPos = new List<Vector3>();

        for (int i = 0; i < brickPosIndexs.Count; i++) 
        {
            brickPos.Add(brickTFs[brickPosIndexs[i]].position);
        } 

        return brickPos;
    }

    public Vector3 GetBridgePos()
    {
        int bridgeIndex = Random.Range(0, bridges.Count);
        return bridges[bridgeIndex].bridgeEndPoint;
    }
}
