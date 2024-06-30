using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Animator animator;
    [SerializeField] Transform body;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] LayerMask groundLayerMask;
    private string currentAnimName = "idle";
    private Vector3 moveDirection;
    private Vector3 nextPosition;
    public int colorIndex; public int ColorIndex => colorIndex;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = JoystickControl.direct.normalized;

        if (moveDirection.magnitude > 0)
        {
            Vector3 checkDirection = transform.position + Vector3.up + moveDirection;

            if (RayCheckGround(checkDirection))
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            }
            body.transform.forward = moveDirection;
            ChangeAnim("run");
        }
        else { ChangeAnim("idle"); }
    }

    private bool RayCheckGround(Vector3 rayPointCheck)
    {
        Debug.DrawRay(rayPointCheck, Vector3.down * 5, Color.red);
        if (Physics.Raycast(rayPointCheck, Vector3.down, out RaycastHit hit, 5f, groundLayerMask))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                nextPosition = hit.point;
                return true;
            }

            if (hit.transform.CompareTag("Stair"))
            {
                hit.transform.TryGetComponent<Stair>(out Stair nextStair);
                if (nextStair.StairColor == colorIndex)
                {
                    nextPosition = hit.point;
                    return true;
                }
                else
                {
                    nextStair.SetStairColor(colorIndex);
                    return false;
                }
            }
        }
        return false;
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    public void SetPlayerColor(int color)
    {
        colorIndex = color;
        skinnedMeshRenderer.material = ColorController.Instance.GetColor(colorIndex);
    }
}
