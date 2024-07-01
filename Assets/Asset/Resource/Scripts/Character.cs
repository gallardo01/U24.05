using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;
    public Transform body;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public LayerMask groundLayerMask;
    public CharacterBrick characterBrick;
    public string currentAnimName = "idle";
    public int colorIndex; public int ColorIndex => colorIndex;

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
}
