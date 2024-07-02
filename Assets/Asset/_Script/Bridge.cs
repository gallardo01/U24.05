using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public int stepFloorColor =-1;
    [SerializeField] GameObject stepFloor;

    public void SetStepFloorColor(int color)
    {
        stepFloorColor = color;
        stepFloor.GetComponent<MeshRenderer>().material = ColorController.Instance.GetColor(color);
    }
}
