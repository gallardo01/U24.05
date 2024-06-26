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
    private void PickBrickOnBackPack()
    {
        GameObject brick = Instantiate(brickPrefabs, backPack.position + backPack.childCount*Vector3.up*0.15f , backPack.rotation);
        brick.GetComponent<Brick>().SetBrickColor(this.colorIndex);
        brick.transform.SetParent(backPack);
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
            }
        }
        if (bridge != null)
        {
            bridge.SetStepFloorColor(this.colorIndex);
        }
    }
}
