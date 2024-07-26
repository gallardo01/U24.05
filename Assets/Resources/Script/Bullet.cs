using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Character self;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Destroy(gameObject, 1f + (0.1f * self.level));

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot") && other.GetComponent<Character>() != self)
        {
            other.GetComponent<Character>().OnDeath();
            self.GainLevel();
        }
    }
}
