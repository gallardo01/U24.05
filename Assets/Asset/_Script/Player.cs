using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform body;
    public SkinnedMeshRenderer character;
    float speed=9f;
    public Animator animator;
    private string currentAnim = "idle";
    public int colorIndex = 0;

    public void SetPlayerColor(int color)
    {
        colorIndex = color;
        character.material = ColorController.Instance.GetColor(colorIndex);
    }
    void Update()
    {
        Vector3 direction = JoystickControl.direct.normalized;
     
        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            body.rotation = newRotation;
            transform.Translate(direction * speed * Time.deltaTime);
            ChangeAnim("run");
        } else
        {
            ChangeAnim("idle");
        }
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
