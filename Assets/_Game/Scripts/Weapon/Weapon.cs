using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Weapon : GameUnit
{
    [SerializeField] protected List<Vector3> listPaths = new List<Vector3>();

    public void InitWeapon(Vector3 startPoint, Vector3 moveDirection, float attackRange)
    {
        SetPath(startPoint, moveDirection, attackRange);
        Move();
    }

    public virtual void SetPath(Vector3 startPoint, Vector3 moveDirection, float attackRange)
    {
        Vector3 targetPoint = startPoint + (moveDirection.normalized * attackRange);

        listPaths.Add(targetPoint);
    }

    public void Move()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < listPaths.Count; i++)
        {
            sequence.Append(transform.DOMove(listPaths[i], 1f).SetEase(Ease.Linear));
        }

        sequence.OnComplete(() =>
        {
            listPaths.Clear();
            SimplePool.Despawn(this);
        });

        sequence.Play();
    }
}
