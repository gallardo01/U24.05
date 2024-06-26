using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 moveDirection;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Animator anim;
    private string currentAnimName;

    [SerializeField] Transform brickParent;
    private Stack<Brick> bricks = new();

    private float brickHeight = 0.2f;

    private Color color;
    [SerializeField] SkinnedMeshRenderer playerMesh;

    [SerializeField] LayerMask stepLayer;

    private int currentFloorIndex;

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
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
            transform.forward = moveDirection;
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        }

        CheckStep();
    }

    private void OnInit()
    {
        currentFloorIndex = 0;
        color = (Color) ColorController.Ins.colorsIndexUsed[0];
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

        LevelManager.Ins.currentLevel.floors[currentFloorIndex].RemoveBrick(brick);
    }

    private bool RemoveBrick()
    {
        if (bricks.Count > 0)
        {
            SimplePool.Despawn(bricks.Pop());

            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckStep()
    {
        RaycastHit hit;
        Vector3 raycastPos = transform.position;
        raycastPos.y += 1f;

        if (Physics.Raycast(raycastPos, Vector3.down, out hit, 10f, stepLayer))
        {
            Step step = hit.collider.GetComponent<Step>();
            if (step.color != color)
            {
                if (RemoveBrick())
                {
                    step.SetStepColor(color);
                    LevelManager.Ins.currentLevel.floors[currentFloorIndex].GenerateBricks(color, 1);
                }          
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = other.GetComponent<Brick>();
            if (color == brick.color)
            {
                AddBrick(brick);
            }
        }
    }
}
