using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow> 
{ 
    public Transform TF;
    public Transform playerTF;
    public Camera gameCamera;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offsetMainMenu;

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
        TF.position = Vector3.Lerp(TF.position, playerTF.position + currentOffset, Time.deltaTime * 5f);
        TF.rotation = Quaternion.Lerp(TF.rotation, currentRotation, Time.deltaTime * 5f);
    }

    public void ChangeState(int state)
    {
        if (state == 1)
        {
            currentOffset = offsetMainMenu;
            currentRotation = rotationMainMenu;
        }
        else if (state == 2) 
        {
            currentOffset = offset;
            currentRotation = rotation;
        } 
    }
}
