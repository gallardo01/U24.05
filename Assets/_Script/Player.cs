using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public float speed = 25f;
    float time;
    //private CounterTime counter = new CounterTime();
    private void Start()
    {
        base.Start();
    }
    void Update()
    {
        time += Time.deltaTime;
        Vector3 direction = JoystickControl.direct.normalized;
        if (direction != Vector3.zero)
        {
            //counter.Cancel();
            Quaternion newRotation = Quaternion.LookRotation(direction);
            body.rotation = newRotation;
            body.Translate(body.forward * Time.deltaTime * speed, Space.World);
            ChangeAnim("run");
            isRunning = true;
            CancelInvoke(nameof(FindTarget));
        }
        else
        {
            ChangeAnim("idle");
            isRunning = false;
            Invoke(nameof(FindTarget),1f);
        }
       
    }
    public void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("bot") && time > cooldownTimeAttack)
            {
                if (isRunning == false)
                {
                    ChangeAnim("attack");
                    isAttack = true;
                    FireWeapon(this.weaponPrefabs, collider.gameObject);
                }
                time = 0;
                break;
            }
        }
    }
}
