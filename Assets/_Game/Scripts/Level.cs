using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Floor> floors = new List<Floor>();

    public void InitLevel()
    {
        //this.PostEvent(EventID.OnInitLevel, startPos.position);
    }
}
