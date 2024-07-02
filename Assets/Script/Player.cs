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
    [SerializeField] private LayerMask bridgeLayer;
    
    
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
        
    }

    private bool CanMove(Vector3 nextpoint)
    {
        RaycastHit hit;
        return Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, planeLayer);
    }
    
    private bool CanJump(Vector3 nextpoint)
    {
        RaycastHit hit;
        return Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, bridgeLayer);
        
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
        }
    }
}
