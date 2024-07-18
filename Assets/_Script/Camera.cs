using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] public Transform TF;
    [SerializeField] Transform TFPlayer;
    public Vector3 offset;
    public static Camera instance;
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        TF.position = TFPlayer.position + offset;
    }
}

