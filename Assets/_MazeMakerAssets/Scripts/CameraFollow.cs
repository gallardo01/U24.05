using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void LateUpdate()
    {
        this.transform.position = player.transform.position + offset;
    }
}
