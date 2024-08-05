using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public LayerMask groundLayer;
    
    private CounterTime counter = new CounterTime();
    // Start is called before the first frame update
    void Start()
    {
        OnInit();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;

        if(direction.magnitude > 0f)
        {
            counter.Cancel();
            transform.Translate(direction * 5f * Time.deltaTime);
            ChangeAnim("run");
            mesh.forward = JoystickControl.direct;
        }
        else if(!isAttack)
        {
            ChangeAnim("idle");
            range.RemoveNullTarget();
            if (range.botsInCircle.Count > 0)
            {
                AttackTarget();
            }
        }
        else
        {
            counter.Execute();  
        }
    }

    public override void OnInit()
    {
        this.enabled = true;    
        isDeath = false;
        gameObject.tag = "Bot";
        ChangeAnim("idle");
        indicator.InitTarget(Color.black, 1, "Player");
        base.OnInit();
    }

    public void AttackTarget()
    {
        isAttack = true;
        Invoke(nameof(ChangeIsAttack), 1.5f);
        ChangeAnim("attack");
        counter.Start(OnAttack, 0.3f);
    }

    private void ChangeIsAttack()
    {
        isAttack = false;
    }

    private bool CheckGround(Transform points)
    {
        RaycastHit hit;
        return Physics.Raycast(points.position, Vector3.down, out hit, 2f, groundLayer);
    }

    public override void OnDeath()
    {
        //Player chet
        counter.Cancel();
        GameController.Instance.EndGame();  
        this.enabled = false;
        UIManager.Instance.OpenAwardUI(level);
        base.OnDeath();
    }

    public void OnDespawn()
    {
        counter.Cancel();
    }
}
