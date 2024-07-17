using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform body;
    public float speed = 5.0f;
    public Animator animator;
    private string currentAinm = "idle";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;
        if (direction != Vector3.zero)
        {
            body.rotation = Quaternion.LookRotation(direction);
            body.Translate(direction * speed * Time.deltaTime, Space.World);
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAinm != animName)
        {
            animator.ResetTrigger(currentAinm);
            currentAinm = animName;
            animator.SetTrigger(currentAinm);
        }
    }
}
