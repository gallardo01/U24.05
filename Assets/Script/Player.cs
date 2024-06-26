using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    private string currentAnim = "idle";
    public Animator animator;
    public int colorIndex = 0;
    public SkinnedMeshRenderer body;
    public Transform mesh;
    

    public GameObject brickStack; // The stack where the bricks will be stored

    private GameObject currentBrick; // The current brick the player is holding
    
    private int totalBrick = 0;
    [SerializeField] private Transform stack;
    [SerializeField] private LayerMask planeLayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StageController stageController = FindObjectOfType<StageController>();
        colorIndex = stageController.randomColorIndices[Random.Range(0, stageController.randomColorIndices.Count)];
        body.material = ColorChange.Instance.GetMaterialColor(colorIndex);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;
        // if (direction != Vector3.zero ) 
        // {
        //     Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        //     transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        //     transform.position += direction * speed * Time.deltaTime;
        //     ChangeAnim("run");
        // }
        if (direction.magnitude > 0f)
        {
            Vector3 nextPoint = transform.position + JoystickControl.direct * Time.deltaTime * speed;
            if (CanMove(nextPoint))
            {
                transform.position = nextPoint;
            }
            ChangeAnim("run");
            mesh.forward = JoystickControl.direct;
        }
        else
        {
            ChangeAnim("idle");
        }
        
        // Vector3 direction = JoystickControl.direct;
        // if (direction != Vector3.zero)
        // {
        //     Ray ray = new Ray(transform.position, Vector3.down);
        //     RaycastHit hit;
        //
        //     // Draw the Raycast in the Unity editor
        //     Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        //
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         // Log the name of the object the Raycast hit
        //         Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
        //
        //         if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Plane") ||
        //             hit.collider.gameObject.layer == LayerMask.NameToLayer("Bridge") ||
        //             hit.collider.gameObject.layer == LayerMask.NameToLayer("Brick"))
        //         {
        //             Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        //             transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        //             transform.position += direction * speed * Time.deltaTime;
        //             ChangeAnim("run");
        //         }
        //         
        //         else
        //         {
        //             // Stop moving if hit object does not have tag "Bridge" or "Plane" or "Stairs"
        //             ChangeAnim("idle");
        //         }
        //     }
        //     
        //     if (currentBrick != null)
        //     {
        //         currentBrick.transform.localPosition = Vector3.zero; // Keep the brick at the center of the brick stack
        //     }
        //     
        //     
        // }
        // else
        // {
        //     ChangeAnim("idle");
        // }
    }

    private bool CanMove(Vector3 nextpoint)
    {
        RaycastHit hit;
        return Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, planeLayer);
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

    private void OnTriggerEnter(Collider collider)
    {
        // Check if the collided object is a brick
        if (collider.gameObject.CompareTag("Brick"))
        {
            // Get the brick's Renderer component
            Renderer brickRenderer = collider.gameObject.GetComponent<Renderer>();

            // Check if the brick's color matches the player's color
            if (brickRenderer != null && brickRenderer.material.color == body.material.color)
            {
                totalBrick++;
                brickRenderer.transform.SetParent(stack);
                brickRenderer.GetComponent<BoxCollider>().enabled = false;
                // Calculate the new position of the brick based on the number of bricks in the stack
                Vector3 newPosition = new Vector3(0, 0 , 0);
                newPosition += new Vector3(0, totalBrick * 0.4f, 0);

                // Move the brick to the new position
                brickRenderer.transform.localPosition = newPosition;
                brickRenderer.transform.localRotation = Quaternion.identity;
                stack.transform.localRotation = (Quaternion.Euler(90,0,0));
                
                // Call the function to respawn the brick after 5 seconds
                StageController stageController = FindObjectOfType<StageController>();
                stageController.RespawnBrick(collider.gameObject, 5f);

                // Update the availableTransforms list
                stageController.UpdateAvailableTransforms();
            }
            else if (collider.gameObject.CompareTag("Bridge"))
            {
                // Apply a force or change the position of the player to simulate climbing
                // This is a simple example, you may need to adjust this to fit your game
                Vector3 climbDirection = new Vector3(0, 1, 0); // Change this to the direction you want the player to climb
                float climbSpeed = 5f; // Change this to control the speed of climbing
                transform.position += climbDirection * climbSpeed * Time.deltaTime;
            }
        }
    }
}
