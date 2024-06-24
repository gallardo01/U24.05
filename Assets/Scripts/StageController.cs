using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageController : Singleton<StageController>
{
    [SerializeField] Brick m_BrickPfb;
    [SerializeField] float timeSpawnNewBrick;

    [SerializeField] Transform m_ContainerBricks;

    private List<Transform> m_BrickContainer = new List<Transform>();
    private List<bool> m_BrickPosMark = new List<bool>();

    private List<Brick> m_Bricks = new List<Brick>();

    void Start()
    {
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

    public void SpawnAllBrick()
    {
        for(int i = 0; i < m_BrickContainer.Count; i++)
        {
            Brick brick = Instantiate(m_BrickPfb, m_BrickContainer[i]);
            brick.index = i;
            m_BrickPosMark[i] = false;
            m_Bricks.Add(brick);
        }
    }

    IEnumerator SpawnBrick(int index)
    {
        yield return new WaitForSeconds(timeSpawnNewBrick);

        Brick brick = Instantiate(m_BrickPfb, m_BrickContainer[index]);
        brick.SetNewMaterial();
        brick.index = index;
        m_Bricks.Add(brick);
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

    public void SpawnNewBrick()
    {
        int pos = GetRandomAvailablePos();

        if (pos != -1)
        {
            StartCoroutine(SpawnBrick(pos));
        }    
    }
    
    public void UpdateBrickPosMark(int index, bool value)
    {
        m_BrickPosMark[index] = value;
    }    
}
