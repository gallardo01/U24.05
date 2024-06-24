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
    

    public GameObject brickStack; // The stack where the bricks will be stored

    private GameObject currentBrick; // The current brick the player is holding
    
    private int totalBrick = 0;
    [SerializeField] private Transform stack;
    
    
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
        if (direction != Vector3.zero ) 
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
            transform.position += direction * speed * Time.deltaTime;
            ChangeAnim("run");
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
                Vector3 newPosition = new Vector3(0, 1, -0.5f);
                newPosition += new Vector3(0, totalBrick * 0.4f, 0);

                // Move the brick to the new position
                brickRenderer.transform.position = newPosition;
                
            }
        }
    }
}
