using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] FloatingJoystick floatingJoystick;
    [SerializeField] Rigidbody rb;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        rb.velocity = direction * speed;
        transform.forward = direction;
    }
}
