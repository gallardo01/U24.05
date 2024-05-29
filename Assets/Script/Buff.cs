using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] int damageBuffAmount = 20;

    private void Update()
    {
        RayCheck(Vector2.up);
        RayCheck(Vector2.down);
        RayCheck(Vector2.right);
        RayCheck(Vector2.left);
    }

    void RayCheck(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10f, playerLayerMask);

        if (hit.collider != null)
        {
            hit.transform.GetComponent<PlayerBehavior>().SetDamage(damageBuffAmount);
        }
    }
}
