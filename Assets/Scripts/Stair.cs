using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public int m_ColorIndex = -1;

    public void SetBrickColor(int index)
    {
        m_ColorIndex = index;
        GetComponent<MeshRenderer>().material = ColorController.Instance.GetMaterialColor(m_ColorIndex);
    }
}
