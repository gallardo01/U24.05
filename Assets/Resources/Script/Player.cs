using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    private string currentAnim = "idle";
    public Transform mesh;
    public int colorIndex = 0;
    public SkinnedMeshRenderer body;
    public Transform bricks;
    private int totalBricks = 0;
    public LayerMask groundLayer;
    public LayerMask stairLayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetPlayerColor(int color)
    {
        colorIndex = color;
        body.material = ColorController.Ins.GetMaterialColor(colorIndex);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;
        
        if (direction.magnitude > 0f)
        {
            Vector3 nextPoint = transform.position + JoystickControl.direct * Time.deltaTime * 5f;
            if(CanMove(nextPoint))
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
        Debug.Log("Ground" + Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, groundLayer));
        Debug.Log("Stair" + Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, stairLayer));
        return Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, groundLayer);
    }

    private Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.5f;
        }
        return transform.position;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Brick" && colorIndex == other.GetComponent<Brick>().brickColor)
        {
            other.gameObject.transform.SetParent(bricks);
            other.gameObject.transform.localPosition = new Vector3(0f, (totalBricks-1) * 0.3f, 0f);
            other.gameObject.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            other.enabled = false;
            totalBricks++;

            StageController.Ins.CreateNewBrick(other.GetComponent<Brick>().brickPosition);
        }
    }
}
