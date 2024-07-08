using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    public SkinnedMeshRenderer character;
    public int colorIndex = 0;
    public float speed = 1f;
    [SerializeField] public Transform backPack;
    [SerializeField] GameObject brickPrefabs;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask stairLayer;
    public Animator animator;
    private string currentAnim = "idle";

    public StageControler stage;
    private void OnEnable()
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
    public void OnTriggerEnter(Collider collision)
    {
        Brick brick = collision.GetComponent<Brick>();
        Bridge bridge = collision.GetComponent<Bridge>();
        if (brick != null )
        {
            if (brick.brickColor == this.colorIndex)
            {
                brick.Removed();
                Destroy(brick.gameObject);
                PickBrickOnBackPack();
                stage.CreatBrickRepeat(brick.brickPosition);
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
        }

        if (collision.CompareTag("Stage"))
        {
            collision.GetComponent<StartStage>().stage.CharacterStartGame(this);
            this.stage = collision.GetComponent<StartStage>().stage;
        }
    }
}
