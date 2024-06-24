using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    private string currentAnim = "idle";
    public Animator animator;

    public GameObject brickStack; // The stack where the bricks will be stored

    private GameObject currentBrick; // The current brick the player is holding
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 direction = JoystickControl.direct;
        // if (direction != Vector3.zero ) 
        // {
        //     Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        //     transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        //     transform.position += direction * speed * Time.deltaTime;
        //     ChangeAnim("run");
        // }
        // else
        // {
        //     ChangeAnim("idle");
        // }
        
        Vector3 direction = JoystickControl.direct;
        if (direction != Vector3.zero)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            // Draw the Raycast in the Unity editor
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

            if (Physics.Raycast(ray, out hit))
            {
                // Log the name of the object the Raycast hit
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Plane") ||
                    hit.collider.gameObject.layer == LayerMask.NameToLayer("Bridge") ||
                    hit.collider.gameObject.layer == LayerMask.NameToLayer("Brick"))
                {
                    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
                    transform.position += direction * speed * Time.deltaTime;
                    ChangeAnim("run");
                }
                
                else
                {
                    // Stop moving if hit object does not have tag "Bridge" or "Plane" or "Stairs"
                    ChangeAnim("idle");
                }
            }
            
            if (currentBrick != null)
            {
                currentBrick.transform.localPosition = Vector3.zero; // Keep the brick at the center of the brick stack
            }
            
            
        }
        else
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
