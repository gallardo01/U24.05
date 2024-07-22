using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Vector3 nextPoints;
    public LayerMask groundLayer;

    private CounterTime counter = new CounterTime();

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        nextPoints = transform.position + JoystickControl.direct * Time.deltaTime * 5f;
        if (CheckGround(nextPoints) && JoystickControl.direct.magnitude > 0f)
        {
            counter.Cancel();
            transform.position = nextPoints;
            transform.forward = JoystickControl.direct;
            ChangeAnim("run");
        }
        else if (!isAttack)
        {
            counter.Execute();
            ChangeAnim("idle");
            range.RemoveNullTarget();
            if (range.botInRange.Count > 0)
            {
                AttackTarget();
            }
        } else
        {
            counter.Execute();
        }
    }

    public void AttackTarget()
    {
        isAttack = true;
        Invoke(nameof(ChangeIsAttack), 1.5f);
        ChangeAnim("attack");
        counter.Start(Throw, 0.5f);
    }

    private void ChangeIsAttack()
    {
        isAttack = false;
    }

    private bool CheckGround(Vector3 points)
    {
        RaycastHit hit;
        return Physics.Raycast(points + Vector3.up * 2, Vector3.down, out hit, 3f, groundLayer);
    }


}
