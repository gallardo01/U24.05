using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorController;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Transform mesh;
    [SerializeField] SkinnedMeshRenderer body;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask stairLayer;
    public Animator animator;
    private string currentAnim = "idle";

    [SerializeField] Transform m_BrickStack;
    private List<Brick> m_BrickCollection = new List<Brick>();

    public int m_ColorIndex;

    void Start()
    {
    }

    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction.Normalize();

        if(direction.magnitude > 0f)
        {
            Vector3 nextPoint = transform.position + JoystickControl.direct * Time.deltaTime * speed;
            if(CanMove(nextPoint))
            {
                //transform.position = nextPoint;
                transform.position = CheckGround(nextPoint);
            }
            //transform.Translate(direction * Time.deltaTime * speed);
            mesh.forward = JoystickControl.direct;
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        } 
    }

    private bool CanMove(Vector3 nextPoint)
    {
        RaycastHit hit;
        //Debug.Log("Ground: " + Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer));
        //Debug.Log("Stair: " + Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stairLayer));
        //Debug.DrawRay(nextPoint, Vector3.down, Color.red, 0.01f);
        return Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer);
    }

    private Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 0.5f;
        }
        return transform.position;
    }

    public void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }    
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Brick")
        {
            Brick brick = collider.GetComponent<Brick>();
            if(brick.m_ColorIndex == m_ColorIndex)
            {
                StageController.Instance.UpdateBrickPosMark(brick.index, true);
                brick.GetComponent<Collider>().enabled = false;
                brick.transform.SetParent(m_BrickStack);
                brick.transform.localRotation = Quaternion.Euler(90, 0, 0);
                brick.transform.localPosition = new Vector3(0, 0, 0.3f * m_BrickCollection.Count);
                m_BrickCollection.Add(brick);
                //StageController.Instance.SpawnNewBrick(brick.m_ColorIndex);
                StageController.Instance.SpawnNewBrickAtPos(brick.index);
            }    
        }    
    }

    public void SetColor(int colorIndex)
    {
        m_ColorIndex = colorIndex;
        body.material = ColorController.Instance.GetMaterialColor(m_ColorIndex);
    }
}
