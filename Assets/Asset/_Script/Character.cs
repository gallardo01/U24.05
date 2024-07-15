using MarchingBytes;
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
        if (currentAnim != animName && currentAnim != "victory")
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
        GameObject brick = EasyObjectPool.Instance.GetObjectFromPool("Brick", backPack.position + backPack.childCount * Vector3.up * 0.15f, backPack.rotation);
        brick.GetComponent<Brick>().SetBrickColor(this.colorIndex);
        brick.transform.SetParent(backPack);
        brick.GetComponent<BoxCollider>().enabled = false;
    }
    public void DestroyBrickOnBackPack()
    {
        Transform lastChild = backPack.GetChild(backPack.childCount - 1);
        EasyObjectPool.Instance.ReturnObjectToPool(lastChild.gameObject);
        lastChild.gameObject.transform.SetParent(null);

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
                EasyObjectPool.Instance.ReturnObjectToPool(brick.gameObject);
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
                    DestroyBrickOnBackPack();
                    bridge.SetStepFloorColor(this.colorIndex);
                }
            }
        }

        if (collision.CompareTag("Stage"))
        {
            collision.GetComponent<StartStage>().stage.CharacterStartGame(this);
            this.stage = collision.GetComponent<StartStage>().stage;
        }
        if (collision.CompareTag("finishPoint"))
        {
            GameController.Instance.EndGame(this);
            ChangeAnim("victory");
        }
    }
}
