using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform body;
    [SerializeField] protected SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] protected LayerMask groundLayerMask;
    public CharacterBrick characterBrick;
    protected string currentAnimName = "idle";
    protected int colorIndex; public int ColorIndex => colorIndex;
    public StageController currentStage;

    protected virtual void Awake()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        characterBrick = GetComponent<CharacterBrick>();
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    public void SetCharacterColor(int color)
    {
        colorIndex = color;
        skinnedMeshRenderer.material = ColorController.Instance.GetColor(colorIndex);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerBox"))
        {
            other.GetComponent<StageTrigger>().stageController.CharacterStartStage(colorIndex);
            currentStage = other.GetComponent<StageTrigger>().stageController;
            if (currentStage.isFinalStage)
            {
                ChangeAnim("victory");
                GameController.Instance.EndGame(this);
            }
        }
    }
}
