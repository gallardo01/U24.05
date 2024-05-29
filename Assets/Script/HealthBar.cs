using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    float hp;
    float maxHP;
    private void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp/maxHP, Time.deltaTime*5f);
    }
    public void OnInit(float maxHP)
    {
        this.maxHP = maxHP;
        hp = maxHP;
        imageFill.fillAmount = 1;
    }
    public void SetNewHP(float hp)
    {
        this.hp = hp;
    }    
}
