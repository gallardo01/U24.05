using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Weapon : GameUnit
{
    [SerializeField] protected int characterLayer;
    [SerializeField] protected int propLayer;

    [SerializeField] protected Character owner;

    [SerializeField] protected List<Vector3> listPaths = new List<Vector3>();

    protected Sequence sequence;

    public void InitWeapon(Character owner, Vector3 startPoint, Vector3 direction, float attackRange)
    {
        this.owner = owner;
        SetPath(startPoint, direction, attackRange);
        Move();
    }

    protected virtual void SetPath(Vector3 startPoint, Vector3 direction, float attackRange)
    {
        Vector3 targetPoint = startPoint + (direction.normalized * attackRange);

        listPaths.Add(targetPoint);
    }

    protected void Move()
    {
        sequence = DOTween.Sequence();

        for (int i = 0; i < listPaths.Count; i++)
        {
            sequence.Append(transform.DOMove(listPaths[i], 1f).SetEase(Ease.Linear));
        }

        sequence.OnComplete(() =>
        {
            OnDespawn();
        });

        sequence.Play();        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == characterLayer)
        {
            Character character = Cache.Ins.GetCachedComponent<Character>(other);
            if (character != owner)
            {
                character.OnDead();
                owner.LevelUp(1);
                OnDespawn();
            }
        }

        if (other.gameObject.layer == propLayer)
        {
            OnDespawn();
        }
    }

    protected void OnDespawn()
    {
        listPaths.Clear();
        sequence.Kill();
        SimplePool.Despawn(this);
    }
}
