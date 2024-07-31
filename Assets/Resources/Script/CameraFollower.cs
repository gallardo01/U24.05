using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    public Transform TF;
    public Transform playerTF;
    public Camera gameCamera;
    [SerializeField] Vector3 offset;
    [SerializeField]  Vector3 offsetMainMenu;
    
    [SerializeField] Quaternion rotation;
    [SerializeField] Quaternion rotationMainMenu;

    private Vector3 currentOffset;
    private Quaternion currentRotation;
    private void Start()
    {
        gameCamera = GetComponent<Camera>();
        ChangeState(1);
    }
    private void LateUpdate()
    {
        if (playerTF != null)
        {
            float lerpFactor = Time.deltaTime * 5f;
            if (lerpFactor > 0 && lerpFactor <= 1)
            {
                TF.position = Vector3.Lerp(TF.position, playerTF.position + currentOffset, lerpFactor);
                TF.rotation = Quaternion.Lerp(TF.rotation, currentRotation, lerpFactor);
            }
        }
    }
    
    
    public void SetCameraSize(float size)
    {
        if (gameCamera.orthographic)
        {
            gameCamera.orthographicSize = size;
        }
        else
        {
            gameCamera.fieldOfView = size;
        }
    }
    
    public void ChangeState(int state)
    {
        if (state == 1)
        {
            currentOffset = offsetMainMenu;
            currentRotation = rotationMainMenu;
            SetCameraSize(2);
        }
        else if( state == 2)
        {
            currentOffset = offset;
            currentRotation = rotation;
            SetCameraSize(10);
        }
    }
}