using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    public SkinnedMeshRenderer character;
    public int colorIndex = 0;
    public float speed = 9f;
    [SerializeField] Transform backPack;
    [SerializeField] GameObject brickPrefabs;
    public Animator animator;
    private string currentAnim = "idle";
    private void Start()
    {
        backPack.transform.Rotate(Vector3.up * 90f);
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
    public void SetPlayerColor(int color)
    {
        colorIndex = color;
        character.material = ColorController.Instance.GetColor(colorIndex);
    }
    private void PickBrickOnBackPack()
    {
        GameObject brick = Instantiate(brickPrefabs, backPack.position + backPack.childCount * Vector3.up * 0.15f, backPack.rotation);
        brick.GetComponent<Brick>().SetBrickColor(this.colorIndex);
        brick.transform.SetParent(backPack);
        brick.GetComponent<BoxCollider>().enabled = false;
    }
    private void DesTroyBrickOnBackPack()
    {
        Transform lastChild = backPack.GetChild(backPack.childCount - 1);
        Destroy(lastChild.gameObject);
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
            }
        }
        if (bridge != null)
        {
            if (backPack.childCount > 0)
            {
                if (this.colorIndex != bridge.stepFloorColor)
                {
                    DesTroyBrickOnBackPack();
                    bridge.SetStepFloorColor(this.colorIndex);
                }
            }
            else
            {
                //Do something
            }
        }
    }
}
