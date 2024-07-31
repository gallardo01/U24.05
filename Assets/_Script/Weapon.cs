using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Character self;
    float timeDissappear = 1f;
    float time;

    private void Start()
    {
        timeDissappear = 1f + (self.level * 0.1f);
    }

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
        Character character = other.GetComponent<Character>();
        Player player = other.GetComponent<Player>();
        if (character != self)
        {
            if (other.CompareTag("bot") || other.CompareTag("player"))
            {
                Destroy(gameObject);
                character.health = character.HPbar.GetComponent<TargetIndicator>().ChangeHealth(-40);
                if (character.health < 0)
                {
                    self.LevelUpPlayer();
                    self.LevelUpData();
                    if (other.CompareTag("player"))
                    {
                        player.OnDeath();
                    }
                    else
                    {
                        character.OnDeath();
                    }
                }
            }
        }
  
    }
}
