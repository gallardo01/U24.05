using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Character self;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot") && other.GetComponent<Character>() != self)
        {
            other.GetComponent<Character>().OnDeath();
            Destroy(gameObject);
        }
    }
}
