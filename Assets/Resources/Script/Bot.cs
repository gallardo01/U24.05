using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    // Một thành phần NavMeshAgent để tìm đường
    public NavMeshAgent agent;
    private IState<Bot> currentState;
    public GameObject targetCircle;

    // Start is called before the first frame update
    
    // Start(): Khởi tạo animation của bot thành "idle" và trang bị một vật phẩm ngẫu nhiên.
    void Start()
    {
        ChangeAnim("idle");
        skin.RandomEquipItem();
    }

    // Update is called once per frame
    // Update(): Thực thi logic của trạng thái hiện tại nếu bot không chết.
    void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }

    // ChangeIsAttackBot(): Lên lịch gọi hàm ResetAttack sau 1 giây.
    public void ChangeIsAttackBot()
    {
        Invoke("ResetAttack", 1f);
    }
    
    // ResetAttack(): Đặt lại trạng thái tấn công của bot.
    private void ResetAttack()
    {
        isAttack = false;
    }
    
    // ChangeState(IState<Bot> state): Thay đổi trạng thái của bot, gọi OnExit trên trạng thái cũ và OnEnter trên trạng thái mới.
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    

    // SetTarget(): Kích hoạt và đặt vị trí của chỉ thị mục tiêu.
    public void SetTarget()
    {
        targetCircle.transform.position = transform.position;
        targetCircle.SetActive(true);
    }
    
    // RemoveTarget(): Vô hiệu hóa chỉ thị mục tiêu.
    public void RemoveTarget()
    {
        targetCircle.SetActive(false);
    }

    //Stop(): Vô hiệu hóa NavMeshAgent và khởi tạo lại bot.
    public void Stop()
    {
        agent.enabled = false;
        OnInit();
    }
    
    //OnDeath(): Xử lý khi bot chết, thay đổi trạng thái thành null, vô hiệu hóa NavMeshAgent, loại bỏ bot khỏi game controller, và bắt đầu coroutine phá hủy.
    public override void OnDeath()
    {
        ChangeState(null);
        agent.enabled = false;
        GameController.Ins.bots.Remove(this);
        base.OnDeath();
        RemoveTarget();
        // Bot chet
        StartCoroutine(DestroyBot());
    }
    
    //DestroyBot(): Coroutine chờ 1.5 giây trước khi phá hủy bot và indicator của nó.
    IEnumerator DestroyBot()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        if (indicator != null)
        {
            Destroy(indicator.gameObject);
        }
    }
    
    //OnAttack(): Gọi phương thức OnAttack của lớp cơ sở.
    public override void OnAttack()
    {
        base.OnAttack();
    }
    
    
    //OnInit(): Khởi tạo trạng thái của bot thành IdleState, vô hiệu hóa indicator mục tiêu, và gọi phương thức OnInit của lớp cơ sở.
    public override void OnInit()
    {
        ChangeState(new IdleState());
        targetCircle.SetActive(false);
        base.OnInit();
    }
}
