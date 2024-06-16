using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void LateUpdate()
    {
        this.transform.position = player.transform.position + offset;
    }
}
