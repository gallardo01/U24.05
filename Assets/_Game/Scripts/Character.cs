using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Vector3 moveDirection;
    protected Vector3 nextPoint;
    [SerializeField] protected float moveSpeed = 5f;

    [SerializeField] protected Transform tf;

    [SerializeField] protected Animator anim;
    protected string currentAnimName;

    [SerializeField] protected Transform brickParent;
    protected Stack<Brick> bricks = new();
    public Stack<Brick> Bricks => bricks;

    protected GameColor color;
    public GameColor Color => color;

    [SerializeField] protected SkinnedMeshRenderer playerMesh;

    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask stepLayer;
    [SerializeField] protected LayerMask bridgeLayer;
    [SerializeField] protected LayerMask gateLayer;

    protected int currentFloor;
    public int CurrentFloor => currentFloor;

    protected int currentStep;

    public virtual void OnInit(GameColor color, Transform transform)
    {
        currentFloor = 0;
        this.color = color;
        tf.position = transform.position;
        playerMesh.material = GameController.Ins.GetMaterialColor(color);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected void AddBrick(Brick brick)
    {
        bricks.Push(brick);
        brick.transform.SetParent(brickParent);
        brick.transform.localEulerAngles = Vector3.zero;
        brick.transform.localPosition = new Vector3(0, (Constants.BRICK_HEIGHT + 0.1f) * bricks.Count, 0);

        brick.brickCollider.enabled = false;

        LevelManager.Ins.currentLevel.floors[currentFloor].RemoveBrick(brick);
    }

    protected bool RemoveBrick()
    {
        if (bricks.Count > 0)
        {
            Brick brick = bricks.Pop();
            brick.brickCollider.enabled = true;
            SimplePool.Despawn(brick);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void ClearBrick()
    {
        int bricksCount = bricks.Count;
        for (int i = 0; i < bricksCount; i++)
        {
            RemoveBrick();
        }
    }

    public bool CanMove(Vector3 nextPoint)
    {
        Vector3 raycastPos = nextPoint;
        raycastPos.y += 4f;

        if (CheckGate(raycastPos))
        {
            return false;
        }

        if (CheckGround(raycastPos))
        {
            return true;
        }

        if (CheckStep(raycastPos) && CheckBridge(raycastPos))
        {
            return true;
        }

        return false;
    }

    protected bool CheckGround(Vector3 raycastPos)
    {
        if (Physics.Raycast(raycastPos, Vector3.down, 5f, groundLayer))
        {
            currentStep = -1;
            return true;
        }

        return false;
    }

    protected bool CheckStep(Vector3 raycastPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPos, Vector3.down, out hit, 5f, stepLayer))
        {
            Step step = Cache.Ins.GetCachedComponent<Step>(hit.collider);

            if (currentStep >= step.StepIndex || step.Color == color)
            {
                currentStep = step.StepIndex;
                return true;
            }

            if (RemoveBrick())
            {
                currentStep = step.StepIndex;
                step.SetStepColor(color);
                LevelManager.Ins.currentLevel.floors[currentFloor].GenerateBrick(color, 1);
                return true;
            }
        }

        return false;
    }

    protected bool CheckBridge(Vector3 raycastPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPos, Vector3.down, out hit, 5f, bridgeLayer))
        {
            nextPoint = hit.point;
            return true;
        }

        return false;
    }

    protected bool CheckGate(Vector3 raycastPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPos, Vector3.down, out hit, 5f, gateLayer))
        {
            Gate gate = Cache.Ins.GetCachedComponent<Gate>(hit.collider);

            if (currentFloor >= gate.Floor)
            {
                return true;
            }
        }

        return false;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = Cache.Ins.GetCachedComponent<Brick>(other);
            if (color == brick.Color)
            {
                AddBrick(brick);
            }
        }

        if (other.CompareTag("Gate"))
        {
            Gate gate = Cache.Ins.GetCachedComponent<Gate>(other);

            if (currentFloor < gate.Floor)
            {
                NextFloor(gate);
            }
        }

        if (other.CompareTag("Finish"))
        {
            tf.position = LevelManager.Ins.currentLevel.finishPos.position;
            LevelManager.Ins.currentLevel.DestroyLevel();
            UIManager.Ins.CloseUI<UIGameplay>();
            this.PostEvent(EventID.OnGameFinish, tf);

            if (this.CompareTag("Player"))
            {
                UIManager.Ins.OpenUI<UIWin>();
            }
            else
            {
                UIManager.Ins.OpenUI<UILose>();
            }
        }
    }

    protected virtual void NextFloor(Gate gate)
    {
        gate.SetGateColor(color);
        LevelManager.Ins.currentLevel.floors[currentFloor].ClearBrick(color);
        currentFloor = gate.Floor;
        LevelManager.Ins.currentLevel.floors[currentFloor].GenerateBrick(color, Constants.QUANTITY_BRICK_PER_COLOR);
        tf.position = gate.BehindPos.position;
    }
}
