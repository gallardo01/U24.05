using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float speed = 5f;
    protected Animator animator;
    protected Transform body;
    protected SkinnedMeshRenderer skinnedMeshRenderer;
    protected LayerMask groundLayerMask;
    protected CharacterBrick characterBrick;
    protected string currentAnimName = "idle";
    protected int colorIndex; public int ColorIndex => colorIndex;
    protected StageController currentStage;

    protected virtual void Awake()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        characterBrick = GetComponent<CharacterBrick>();
    }

    protected void ChangeAnim(string animName)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Stage>(out Stage stage))
        {
            currentStage = stage.StageController;
        }
    }
}
