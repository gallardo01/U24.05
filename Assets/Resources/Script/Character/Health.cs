using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isPlayer;
    private int currentHealth;


    public void TakeDamage(int damage)
    {
        int realDamageTaken = Mathf.Clamp(damage, currentHealth, damage);
        currentHealth -= realDamageTaken;

        if(currentHealth <= 0)
        {
            HandleDeath(GetComponent<Character>());
        }
    }

    public void HandleDeath(Character charater)
    {   
        charater.OnDeath();
        if(!isPlayer)
        {
            LeanPool.Despawn(gameObject, 2f);
        }
    }
}
