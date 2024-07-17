using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] Rigidbody rb;

    private WeaponType weaponType;
    private int level = 0;

    List<Character> characters = new List<Character>();

    public void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
