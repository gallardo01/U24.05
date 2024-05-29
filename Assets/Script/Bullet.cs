using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("monster"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            UImanager.instance.EarnCoin();
        }
    }
}
