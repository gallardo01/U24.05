using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    public InputAction fire;
    private float cooldownTime = 3f;
    private float time;
    public float speed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        this.HPbar.GetComponent<HPbar>().SetHP();
        fire.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Vector3 direction = JoystickControl.direct.normalized;
        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            body.rotation = newRotation;
            body.Translate(body.forward * Time.deltaTime * speed, Space.World);
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        }
        if (fire.triggered && time > cooldownTime)
        {
            FireWeapon();
            time = 0;
            this.health = this.HPbar.GetComponent<HPbar>().ChangeHealth(-10);
        }
    }
}
