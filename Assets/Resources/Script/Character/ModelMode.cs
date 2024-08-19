using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelMode : MonoBehaviour, IGameStateListener
{
    [SerializeField] private Animator animator;
    private string currentAnimName = "";

    private const string Idle = "idle", Run = "run", Attack = "attack", Dance = "dance", Ulti = "ulti" , Win = "win";
    private string[] AnimNames = new string[] { Idle, Run, Attack, Dance, Ulti, Win}; 
    //public float rotationSpeed = 0.2f;

    //private Vector2 startTouchPosition;
    //private Vector2 currentTouchPosition;
    //private bool isSwiping = false;

    //void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            Start of swipe
    //           startTouchPosition = touch.position;
    //            isSwiping = true;
    //        }
    //        else if (touch.phase == TouchPhase.Moved)
    //        {
    //            Continue swipe
    //            currentTouchPosition = touch.position;

    //            if (isSwiping)
    //            {
    //                Calculate swipe distance
    //               Vector2 swipeDelta = currentTouchPosition - startTouchPosition;

    //                Rotate the model around the Y-axis
    //                transform.Rotate(0f, -swipeDelta.x * rotationSpeed, 0f);

    //                Update the start touch position to the current position
    //               startTouchPosition = currentTouchPosition;
    //            }
    //        }
    //        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
    //        {
    //            End of swipe
    //           isSwiping = false;
    //        }
    //    }
    //}

    public float rotationSpeed = 0.2f; // Adjust the speed of rotation

    private Vector3 previousMousePosition;
    private bool isDragging = false;

    void Update()
    {
        // Check if the left mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            previousMousePosition = Input.mousePosition;
        }

        // Check if the left mouse button is held down
        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - previousMousePosition;

            // Rotate the model around the Y-axis based on the horizontal movement of the mouse
            transform.Rotate(0f, -mouseDelta.x * rotationSpeed, 0f);

            // Update the previous mouse position to the current position
            previousMousePosition = Input.mousePosition;
        }

        // Check if the left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    public void PlayRandomAnim() 
    {
        ChangAnim(AnimNames[Random.Range(0, AnimNames.Length)]);
    }

    public void ChangAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    // Turn On Off
    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.SHOP:
                this.enabled = true;
                break;
            case GameState.GAME:
                this.enabled = false;
                break;
            case GameState.MENU:
                this.enabled = false;
                break;
        }
    }
}
