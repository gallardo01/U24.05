using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform tf;
    [SerializeField] protected Transform weaponHoldParent;
    [SerializeField] protected Transform weaponStartPoint;
    [SerializeField] protected SphereCollider sphereCollider;

    protected string currentAnimName;

    protected int level = 0;
    protected float baseAttackRange = 5f;

    protected List<Character> targetInRange = new List<Character>();

    protected WeaponType weaponType;

    protected float levelScale => 1.0f + level * 0.05f;
    protected float attackRange => baseAttackRange * levelScale;

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void AddTarget(Character character)
    {
        targetInRange.Add(character);
    }

    public void RemoveTarget(Character character)
    {
        targetInRange.Remove(character);
    }
}

