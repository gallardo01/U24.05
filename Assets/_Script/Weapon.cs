using MarchingBytes;
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
            SetForceRigibody();
            EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
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
                SetForceRigibody();
                EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
                int damage = self.attack - character.defend;
                character.health = character.HPbar.GetComponent<TargetIndicator>().ChangeHealth(-damage);
                if (character.health < 1)
                {
                    self.LevelUpPlayer();
                    if (self.GetComponent<Player>())
                    {
                        Debug.Log("camera up");
                        Camera.instance.SetLevelOffsetCamera(self.level);
                    }
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
    public void SetTimeDisappear()
    {
        timeDissappear = 1f + (self.level * 0.1f);
        time = 0f;
    }

    public void SetForceRigibody()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
