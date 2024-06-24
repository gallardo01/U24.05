using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] Material m_Material;

    public int index;
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
}
