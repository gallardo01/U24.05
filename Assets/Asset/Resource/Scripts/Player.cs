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
    private int colorIndex; public int ColorIndex => colorIndex;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        moveDirection = JoystickControl.direct.normalized;

        if (moveDirection.magnitude > 0)
        {
            Vector3 checkDirection = transform.position + Vector3.up + moveDirection * speed * Time.deltaTime;

            if (RayCheckGround(checkDirection))
            {
                transform.position = nextPosition;
            }
            body.transform.forward = moveDirection;
            ChangeAnim("run");
        }
        else { ChangeAnim("idle"); }
    }

    private bool RayCheckGround(Vector3 rayPointCheck)
    {
        if (Physics.Raycast(rayPointCheck, Vector3.down, out RaycastHit hit, 5f, groundLayerMask))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                Debug.Log("Ground");
                nextPosition = hit.point;
                return true;
            }

            if (hit.transform.CompareTag("Stair"))
            {
                Debug.Log("Stair");
                hit.transform.TryGetComponent<Stair>(out Stair nextStair);
                nextStair.SetStairColor(colorIndex);
                nextPosition = hit.point;
                if (nextStair.StairColor == colorIndex) return true;
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
