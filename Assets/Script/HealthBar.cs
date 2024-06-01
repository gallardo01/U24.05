using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeSlider;
    public float maxHealth = 100f;
    public float health;
    public float lerpSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthSlider.maxValue;
        health = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != health ){
            healthSlider.value = health;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            takeDamage(10);
        }
        if(healthSlider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, health, lerpSpeed);
        }
    }

    private void takeDamage(float damage)
    {
        health -= damage;
    }
}
