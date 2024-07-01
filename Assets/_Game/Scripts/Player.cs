using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 moveDirection;
    Vector3 nextPoint;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Animator anim;
    private string currentAnimName;

    [SerializeField] Transform brickParent;
    private Stack<Brick> bricks = new();

    private float brickHeight = 0.2f;

    private Color color;
    [SerializeField] SkinnedMeshRenderer playerMesh;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask stepLayer;
    [SerializeField] LayerMask bridgeLayer;
    [SerializeField] LayerMask gateLayer;

    [SerializeField] int currentFloor;

    private int currentStep;

    void Start()
    {
        OnInit();       
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = JoystickControl.direct;

        if (moveDirection.magnitude > 0)
        {
            nextPoint = transform.position + moveDirection * Time.deltaTime * moveSpeed;
            
            if (CanMove(nextPoint))
            {
                transform.position = nextPoint;
            }

            transform.forward = moveDirection;
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        }

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    AudioManager.Ins.PlaySFX(SFXType.Canon);
        //}
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    AudioManager.Ins.PlaySFX(SFXType.CloseRevive);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    AudioManager.Ins.PlaySFX(SFXType.ButtonClick);
        //}
    }   

    private void OnInit()
    {
        currentFloor = 0;
        color = ColorController.Ins.colorsUsed[0];
        playerMesh.material = ColorController.Ins.GetMaterialColor(color);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    private void AddBrick(Brick brick)
    {
        bricks.Push(brick);
        brick.transform.SetParent(brickParent);
        brick.transform.localEulerAngles = Vector3.zero;
        brick.transform.localPosition = new Vector3(0, (brickHeight + 0.1f) * bricks.Count, 0);

        brick.brickCollider.enabled = false;

        LevelManager.Ins.currentLevel.floors[currentFloor].RemoveBrick(brick);
    }

    private bool RemoveBrick()
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

    private bool CanMove(Vector3 nextPoint)
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

    private bool CheckGround(Vector3 raycastPos)
    {
        if (Physics.Raycast(raycastPos, Vector3.down, 5f, groundLayer))
        {
            currentStep = -1;
            return true;
        }

        return false;
    }

    private bool CheckStep(Vector3 raycastPos)
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

    private bool CheckBridge(Vector3 raycastPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastPos, Vector3.down, out hit, 5f, bridgeLayer))
        {
            nextPoint = hit.point;
            return true;
        }

        return false;
    }

    private bool CheckGate(Vector3 raycastPos)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = Cache.Ins.GetCachedComponent<Brick>(other);
            if (color == brick.Color)
            {
                AddBrick(brick);
            }
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            Gate gate = Cache.Ins.GetCachedComponent<Gate>(other);

            if (currentFloor < gate.Floor)
            {
                gate.SetGateColor(color);
                LevelManager.Ins.currentLevel.floors[currentFloor].ClearBrick(color);
                currentFloor = gate.Floor;
                LevelManager.Ins.currentLevel.floors[currentFloor].GenerateBrick(color, Constants.QUANTITY_BRICK_PER_COLOR);
            }            
        }
    }
}
