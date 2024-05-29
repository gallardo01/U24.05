using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        StartCoroutine(AutoDestroy());
    }


    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            collision.GetComponent<MonsterController>().OnDeath();
            GameController.Instance.AddCoin(1);
            Destroy(gameObject);
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("AutoDestroy...");
        Destroy(gameObject);
    }    
}
