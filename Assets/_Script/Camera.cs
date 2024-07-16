using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform TF;
    [SerializeField] Transform TFPlayer;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        TF.position = TFPlayer.position + offset;
    }
}

