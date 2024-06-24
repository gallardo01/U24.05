using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorController;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Transform mesh;
    [SerializeField] SkinnedMeshRenderer body;
    public Animator animator;
    private string currentAnim = "idle";

    [SerializeField] Transform m_BrickStack;
    private List<Brick> m_BrickCollection = new List<Brick>();

    public int m_ColorIndex;

    void Start()
    {
        m_ColorIndex = Random.Range(0, (int)Colors.Count);
        body.material = ColorController.Instance.GetMaterialColor(m_ColorIndex);
    }

    void Update()
    {
        Vector3 direction = JoystickControl.direct;
        direction.Normalize();

        if(direction.magnitude > 0f)
        {
            transform.Translate(direction * Time.deltaTime * speed);
            mesh.forward = JoystickControl.direct;
            ChangeAnim("run");
        }
        else
        {
            ChangeAnim("idle");
        } 
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
            StageController.Instance.UpdateBrickPosMark(brick.index, true);
            brick.transform.SetParent(m_BrickStack);
            brick.transform.localRotation = Quaternion.Euler(90, 0, 0);
            brick.transform.localPosition = new Vector3(0, 0, 0.3f * m_BrickCollection.Count);
            m_BrickCollection.Add(brick);
            StageController.Instance.SpawnNewBrick(brick.m_ColorIndex);
        }    
    }
}
