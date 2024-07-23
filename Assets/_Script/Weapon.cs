using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Character self;
    float timeDissappear = 1f;
    float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.Rotate(Vector3.forward * 250f * Time.deltaTime);
        if (time > timeDissappear)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Bot bot = other.GetComponent<Bot>();
        if (other.CompareTag("bot") && other.GetComponent<Character>() != self || other.CompareTag("player") && other.GetComponent<Character>() != self)
        {
            Destroy(gameObject);
            bot.health = bot.HPbar.GetComponent<HPbar>().ChangeHealth(-10);
            float healthBot = other.GetComponent<Bot>().health;
            if (healthBot < 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
