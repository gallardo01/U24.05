using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    private CounterTime counter = new CounterTime();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;
        if (direction != Vector3.zero)
        {
            counter.Cancel();
            body.rotation = Quaternion.LookRotation(direction);
            body.Translate(direction * speed * Time.deltaTime, Space.World);
            ChangeAnim("run");
        } 
        else  if (!isAttack)
        {
            counter.Excute();
            ChangeAnim("idle");
            characterRange.RemoveNullTarget();
            if (characterRange.botInRange.Count > 0)
            {
                AttackTarget();
            }
        } else
        {
            counter.Excute();
        }
        
    }
    
    public void AttackTarget()
    {
        isAttack = true;
        Invoke("ChangeIsAttack", 1.5f);
        ChangeAnim("attack");
        counter.Start(Throw, 0.5f);
    }

    private void ChangeIsAttack()
    {
        isAttack = false;
    }


   
}
