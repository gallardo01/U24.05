using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ColorController;

public class StageController : Singleton<StageController>
{
    [SerializeField] Brick m_BrickPfb;
    [SerializeField] float timeSpawnNewBrick;
    [SerializeField] Transform m_ContainerBricks;

    private List<Transform> m_BrickContainer = new List<Transform>();
    private List<bool> m_BrickPosMark = new List<bool>();

    private List<Brick> m_Bricks = new List<Brick>();
    private List<int> m_ListColorIndex = new List<int>();

    Dictionary<int, int> m_TotalBrickColor = new Dictionary<int, int>();

    void Start()
    {
        m_ListColorIndex = GetRandomColorIndex(4);
        //foreach(var c in m_ListColorIndex)
        //{
        //    Debug.Log(((Colors)c).ToString());
        //}

        OnInit();
        SpawnAllBrick();

    }

    public void OnInit()
    {
        m_BrickContainer.Clear();
        int index = 0; 
        foreach (Transform transform in m_ContainerBricks.GetComponentsInChildren<Transform>())
        {
            if(index == 0)
            {
                index++;
                continue;
            }    
            m_BrickContainer.Add(transform);
            m_BrickPosMark.Add(false);
        }
    }    

    private void InitBricksColor()
    {
        int totalColor = m_BrickPosMark.Count / m_ListColorIndex.Count;

        for(int i = 0; i < m_ListColorIndex.Count; i++)
        {
            m_TotalBrickColor.Add(m_ListColorIndex[i], totalColor);
        }    
        
        for(int i = 0; i < m_Bricks.Count; i++)
        {
            while(true)
            {
                int brickColor = Random.Range(0, m_ListColorIndex.Count);
                if(m_TotalBrickColor[m_ListColorIndex[brickColor]] > 0)
                {
                    m_TotalBrickColor[m_ListColorIndex[brickColor]]--;
                    m_Bricks[i].SetColor(m_ListColorIndex[brickColor]);
                    break;
                }    
            }    
        }    
    }    

    public void SpawnAllBrick()
    {
        for(int i = 0; i < m_BrickContainer.Count; i++)
        {
            Brick brick = Instantiate(m_BrickPfb, m_BrickContainer[i]);
            brick.index = i;
            m_BrickPosMark[i] = false;
            m_Bricks.Add(brick);
        }

        InitBricksColor();
    }

    IEnumerator SpawnBrick(int index, int colorIndex)
    {
        yield return new WaitForSeconds(timeSpawnNewBrick);

        Brick brick = Instantiate(m_BrickPfb, m_BrickContainer[index]);
        brick.SetColor(colorIndex);
        brick.index = index;
    }    

    public int GetRandomAvailablePos()
    {
        if (m_BrickPosMark.FindAll(x => x == true).Count == 0) return -1;

        while(true)
        {
            int pos = Random.Range(0, m_BrickPosMark.Count);
            if(m_BrickPosMark[pos])
            {
                m_BrickPosMark[pos] = false;
                return pos;
            }    
        }
    }    

    public void SpawnNewBrick(int colorIndex)
    {
        int pos = GetRandomAvailablePos();

        if (pos != -1)
        {
            StartCoroutine(SpawnBrick(pos, colorIndex));
        }    
    }
    
    public void UpdateBrickPosMark(int index, bool value)
    {
        m_BrickPosMark[index] = value;
    }    

    public List<int> GetRandomColorIndex(int quantity)
    {
        if (quantity > (int)Colors.Count - 1) return null;

        List<int> result = new List<int>();

        while(result.Count < quantity)
        {
            int index = Random.Range(0, (int)Colors.Count - 1);
            if(!result.Contains(index))
            {
                result.Add(index);
            }
        }
        return result;
    }

    public int GetRandomBrickColor()
    {
        int colorIndex = Random.Range(0, m_ListColorIndex.Count);
        return m_ListColorIndex[colorIndex];
    }
}

