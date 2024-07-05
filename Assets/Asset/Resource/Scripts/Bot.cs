using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    [SerializeField] private LayerMask brickLayerMask;
    private IState currentState;
    private Vector3 target;

    protected override void OnInit()
    {
        base.OnInit();
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
        transform.position = Vector3.MoveTowards(transform.position, this.target, speed * Time.deltaTime);
    }

    //public void FindCurrentBricks()
    //{
    //    Collider[] bricks = Physics.OverlapSphere(this.transform.position, 100f, brickLayerMask);

    //    List<Brick> sameColorBricks = new List<Brick>();
    //    for (int i = 0; i < bricks.Length; i++)
    //    {
    //        bricks[i].TryGetComponent<Brick>(out Brick checkBrick);
    //        if (colorIndex == checkBrick.BrickColor)
    //        {
    //            sameColorBricks.Add(checkBrick);
    //        }
    //    }

    //    float min = float.MaxValue;
    //    Brick currentBrick = null;
    //    for (int i = 0; i < sameColorBricks.Count; i++)
    //    {
    //        float distance = Vector3.Distance(this.transform.position, sameColorBricks[i].transform.position);
    //        if (distance < min)
    //        {
    //            min = distance;
    //            currentBrick = sameColorBricks[i];
    //        }
    //    }
    //    target = currentBrick.transform.position;
    //}
}
