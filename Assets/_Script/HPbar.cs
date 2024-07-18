using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject player;
    Transform cameraTransform;
    float maxHP;
    float health;
    private void Start()
    {
        cameraTransform = Camera.instance.TF;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
    }
    public void SetHP()
    {
        maxHP = player.GetComponent<Character>().maxHP;
        health = player.GetComponent<Character>().health;
    }

    public float ChangeHealth(float hp)
    {
        health += hp;
        if (health <= maxHP)
        {
            slider.value = health / maxHP;
        } else
        {
            health = maxHP;
        }   
        return health;
    }
}
