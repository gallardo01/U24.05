using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField] Transform body;
    [SerializeField] Transform backPack;
    [SerializeField] GameObject brickPrefabs;
    [SerializeField] GameObject stepFloorPrefabs;
    [SerializeField] LayerMask groundLayer;
    float speed=9f;
    public Animator animator;
    private string currentAnim = "idle";

    private void Start()
    {
        backPack.transform.Rotate(Vector3.up * 90f);
    }
    void Update()
    {
        Vector3 direction = JoystickControl.direct.normalized;
     
        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            body.rotation = newRotation;
            Vector3 nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if (CanMove(nextPoint))
            {
                transform.position = nextPoint;
            }
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
    private Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up*1.1f;
        }
        return Vector3.zero;
    }
    private bool CanMove(Vector3 nextpoint)
    {
        RaycastHit hit;
        Debug.DrawLine(nextpoint, nextpoint + Vector3.down * 2f, Color.red, 2f);
        return Physics.Raycast(nextpoint, Vector3.down,out hit, 2f, groundLayer);
    }
    private void PickBrickOnBackPack()
    {
        GameObject brick = Instantiate(brickPrefabs, backPack.position + backPack.childCount*Vector3.up*0.15f , backPack.rotation);
        brick.GetComponent<Brick>().SetBrickColor(this.colorIndex);
        brick.transform.SetParent(backPack);
        brick.GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        Brick brick = collision.GetComponent<Brick>();
        Bridge bridge = collision.GetComponent<Bridge>();
        if (brick != null)
        {
            Destroy(brick);
            if (brick.brickColor == this.colorIndex)
            {
                Destroy(brick.gameObject);
                PickBrickOnBackPack();
                StageControler.Instance.CreatBrickRepeat(brick.brickPosition);
                Debug.Log(brick.brickPosition);
            }
        }
        if (bridge != null)
        {
            bridge.SetStepFloorColor(this.colorIndex);
        }
    }
}
