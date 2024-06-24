using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] Brick m_BrickPfb;

    [SerializeField] Transform m_ContainerBricks;

    private List<Transform> m_BrickPos = new List<Transform>();
    private List<Brick> m_Bricks = new List<Brick>();

    void Start()
    {
        OnInit();
        SpawnBrick();
    }

    public void OnInit()
    {
        m_BrickPos.Clear();
        int index = 0; 
        foreach (Transform transform in m_ContainerBricks.GetComponentsInChildren<Transform>())
        {
            if(index == 0)
            {
                index++;
                continue;
            }    
            m_BrickPos.Add(transform);
        }
    }    

    public void SpawnBrick()
    {
        foreach(Transform transform in m_BrickPos)
        {
            Brick brick = Instantiate(m_BrickPfb, transform);
            m_Bricks.Add(brick);
        }    
    }    
}
