using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isPlayer;
    private int currentHealth;
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }


    public void TakeDamage(int damage, Character whoProjectile)
    {
        int realDamageTaken = Mathf.Clamp(damage, currentHealth, damage);
        currentHealth -= realDamageTaken;

        if(currentHealth <= 0)
        {
            HandleDeath(character, whoProjectile);
        }
    }

    public void HandleDeath(Character charater, Character killerCharacter)
    {
        if(!isPlayer) LeanPool.Despawn(gameObject, 2f);
        charater.OnDeath(killerCharacter);
    }
}
