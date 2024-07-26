using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    public Transform TF;
    public Transform playerTF;
    public Camera gameCamera;
    [SerializeField] Vector3 offset;

    private void Start()
    {
        gameCamera = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (playerTF != null)
        {
            TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 5f);
        }
    }
}