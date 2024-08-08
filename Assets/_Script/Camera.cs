using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Camera : MonoBehaviour
{
    [SerializeField] public Transform TF;
    [SerializeField] Transform TFPlayer;

    [SerializeField] public Vector3 offset;
    [SerializeField] public Vector3 offsetMainMenu;
    [SerializeField] public Vector3 offsetShop;

    [SerializeField] public Quaternion rotation;
    [SerializeField] public Quaternion rotationMainMenu;
    public static Camera instance;

    private Vector3 currentOffset;
    private Quaternion currentRotation;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ChangeState(1);
    }
    // Update is called once per frame
    void Update()
    {
        TF.position = TFPlayer.position + currentOffset;
        TF.rotation = Quaternion.Lerp(transform.rotation, currentRotation, Time.deltaTime*4f);
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
            currentOffset = offsetShop;
            currentRotation = rotationMainMenu;
        }
        else
        {
            currentOffset = offset;
            currentRotation = rotation;
        }
    }
}

