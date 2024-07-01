using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Vector3 moveDirection;
    private Vector3 nextPosition;

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        moveDirection = JoystickControl.direct.normalized;

        if (moveDirection.magnitude > 0)
        {
            Vector3 checkDirection = transform.position + Vector3.up + moveDirection;

            if (RayCheckMove(checkDirection))
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            }
            body.transform.forward = moveDirection;
            ChangeAnim("run");
        }
        else { ChangeAnim("idle"); }
    }

    private bool RayCheckMove(Vector3 rayPointCheck)
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
                else if (characterBrick.CharacterBrickNumbers > 0)
                {
                    nextStair.SetStairColor(colorIndex);
                    characterBrick.RemoveBrick();
                }
            }
        }
        return false;
    }
}
