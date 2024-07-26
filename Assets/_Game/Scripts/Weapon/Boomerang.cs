using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{
    private void Update()
    {
        tf.Rotate(720f * Time.deltaTime * Vector3.up);
    }

    protected override void SetPath(Vector3 startPoint, Vector3 moveDirection, float attackRange)
    {
        base.SetPath(startPoint, moveDirection, attackRange);
        listPaths.Add(startPoint);
    }
}
