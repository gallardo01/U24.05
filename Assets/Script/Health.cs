using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] MonsterMovement monster;
    [SerializeField] Image healthBar;
    [SerializeField] int maxHeath = 100;
    [SerializeField] int monsterGoldReward = 1 ;


    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHeath;
        ShowHealth();
    }

    internal void TakeDame(int damage)
    {
        currentHealth -= damage;
        ShowHealth();

        if (currentHealth <= 0)
        {
            MonsterDeath();
        }
    }

    private void ShowHealth()
    {
        healthBar.fillAmount = (float)currentHealth / maxHeath;
    }

    private void MonsterDeath()
    {
        GameController.Instance.AddGold(monsterGoldReward);
        Destroy(this.gameObject);
    }
}
