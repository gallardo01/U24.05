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

        if (isDead == false)
        {
            if (direction != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(direction);
                body.rotation = newRotation;
                body.Translate(body.forward * Time.deltaTime * speed, Space.World);
                ChangeAnim("run");
                isRunning = true;
            }
            else
            {
                ChangeAnim("idle");
                isRunning = false;
                FindTarget();
            }
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("bot"))
            {
                if (Vector3.Distance(transform.position, collider.transform.position) < detectionRadius)
                {
                    collider.GetComponent<Bot>().inAreaAtack.SetActive(true);
                }
                else
                {
                    collider.GetComponent<Bot>().inAreaAtack.SetActive(false);
                }
            }
        }
    }
    public void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("bot") && time > cooldownTimeAttack)
            {
                if (isRunning == false )
                {
                    ChangeAnim("attack");
                    isAttack = true;
                    target = collider.GetComponent<Character>().gameObject;
                    OnAttack();
                }
                time = 0;
                break;
            }
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GameController.instance.EndGame();
    }
}
