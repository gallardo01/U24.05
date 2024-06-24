using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorController;

public class Brick : MonoBehaviour
{
    [SerializeField] Material m_Material;

    public int index;
    public int m_ColorIndex;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();

        if(renderer != null)
        {
            renderer.material = m_Material;
        }    
    }   
    
    public void SetColor(int index)
    {
        m_ColorIndex = index;
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material = ColorController.Instance.GetMaterialColor(m_ColorIndex);
        }
    }
}
