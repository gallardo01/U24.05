using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Duplicate : MonoBehaviour
{
    [SerializeField] Transform obj;
    [SerializeField] Transform objParent;

    Transform temp;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            temp = (Transform)PrefabUtility.InstantiatePrefab(obj, objParent);
            temp.transform.localPosition = new Vector3(0, 0.3f, 0.8f) * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
