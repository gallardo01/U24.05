using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBrick : MonoBehaviour
{
    [SerializeField] Material[] material; 
    // Start is called before the first frame update
    void Start()
    {
        int pick = PlayerPrefs.GetInt("Pick");
        gameObject.GetComponent<MeshRenderer>().material = material[pick - 1];
    }

    
}
