using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public float speed = 25f;


    // Start is called before the first frame update
    void Start()
    {
        this.HPbar.GetComponent<HPbar>().SetHP();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Vector3 direction = JoystickControl.direct.normalized;
        if (direction != Vector3.zero && isAttack == false)
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

        if (time > 0.9f)
        {
            isAttack = false;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("bot") && time > cooldownTime)
            {
                isAttack = true;
                FireWeapon(this.weaponPrefabs);
                time = 0;
                break;
            }
        }
    }
}
