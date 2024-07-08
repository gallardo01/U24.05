using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    private string currentAnim = "idle";
    public Transform mesh;
    public int colorIndex = 0;
    public SkinnedMeshRenderer body;
    public Transform bricks;
    public int totalBricks = 0;
    public LayerMask groundLayer;
    public LayerMask stairLayer;
    private List<Brick> listBricks = new List<Brick>();
    public StageController stage;

    public void RemoveBrick()
    {
        if (totalBricks > 0)
        {
            Destroy(listBricks[totalBricks - 1].gameObject);
            listBricks.RemoveAt(totalBricks - 1);
            totalBricks--;
        }
    }
    public void SetCharacterColor(int color)
    {
        colorIndex = color;
        body.material = ColorController.Ins.GetMaterialColor(colorIndex);
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
            other.gameObject.transform.localPosition = new Vector3(0f, (totalBricks - 1) * 0.3f, 0f);
            other.gameObject.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            other.enabled = false;
            other.GetComponent<Brick>().RemoveBricks();
            listBricks.Add(other.GetComponent<Brick>());
            totalBricks++;
            other.GetComponent<Brick>().stage.CreateNewBrick(other.GetComponent<Brick>().brickPosition);
            //StageController.Ins.CreateNewBrick(other.GetComponent<Brick>().brickPosition);
        }

        if (other.gameObject.tag == "Stage")
        {
            this.stage = other.gameObject.GetComponent<Stage>().stage;
            other.gameObject.GetComponent<Stage>().stage.CharacterStartGame(colorIndex);
            //StageController.Ins.CharacterStartGame(colorIndex);
        }
    }

    public bool CanMove(Vector3 nextpoint)
    {
        RaycastHit hit;
        //Debug.Log("Ground" + Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, groundLayer));
        //Debug.Log("Stair" + Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, stairLayer));
        if (Physics.Raycast(nextpoint, Vector3.down, out hit, 2f, stairLayer))
        {
            int stairColor = hit.collider.gameObject.GetComponent<Stair>().stairColor;
            if (colorIndex != stairColor)
            {
                // Check con` gach hay k
                if (totalBricks > 0)
                {
                    RemoveBrick();
                    // Tha gach
                    hit.collider.gameObject.GetComponent<Stair>().SetStairColor(colorIndex);
                }
                return false;
            }
        }
        return Physics.Raycast(nextpoint, Vector3.down, groundLayer);
    }
}
