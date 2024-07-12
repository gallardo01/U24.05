using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Bot : Character
{
    [SerializeField] private LayerMask brickLayerMask;
    private IState currentState;
    private Vector3 target;
    public NavMeshAgent navMeshAgent;

    protected override void OnInit()
    {
        base.OnInit();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = new IdleState();
        ChangeState(currentState);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void BotMovement(Vector3 target)
    {
        this.target = target;
        navMeshAgent.SetDestination(this.target);
        ChangeAnim("run");
    }

    public int GetBotBrick() => characterBrick.BrickNumbers;
    public void RayCheckBrige()
    {
        Debug.DrawRay(transform.position, Vector3.down * 5, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f, groundLayerMask))
        {
            if (hit.transform.CompareTag("LadderStep"))
            {
                hit.transform.TryGetComponent<LadderStep>(out LadderStep nextStair);
                if (nextStair.StepColor == colorIndex) return;
                else if (characterBrick.BrickNumbers > 0)
                {
                    nextStair.SetStairColor(colorIndex);
                    characterBrick.RemoveBrick();
                }
            }
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
