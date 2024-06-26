using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] Animator animator;
    [SerializeField] Transform body;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] LayerMask groundLayerMask;
    private string currentAnimName = "idle";
    private Vector3 moveDirection;
    private int colorIndex; public int ColorIndex => colorIndex;

    void Update()
    {
        moveDirection = JoystickControl.direct.normalized;

        if(moveDirection.magnitude > 0)
        {
            Vector3 nextPosition = transform.position + moveDirection * speed * Time.deltaTime;
            if(RayCheck(nextPosition))
            {
                transform.position = nextPosition;
            }
            body.transform.forward = moveDirection;
            ChangeAnim("run");
        }
        else { ChangeAnim("idle"); }
    }

    private bool RayCheck(Vector3 nextPosition)
    {
        return (Physics.Raycast(nextPosition, Vector3.down, 5f, groundLayerMask));
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
