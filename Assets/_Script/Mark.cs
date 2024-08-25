using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public GameObject markPlayer;
    public Bot self;

    private void Update()
    {
        if (!self)
        {
            Destroy(this.gameObject);
        }
    }
}
