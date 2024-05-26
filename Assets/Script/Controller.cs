using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject monsterPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(CreateNewObject), 1f,1f);
    }
    private void CreateNewObject()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
