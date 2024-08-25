using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{

    public float speed = 17f;
    float time;
    public Stats stats;
    public RadarIndicator radarIndicator;
    //private CounterTime counter = new CounterTime();
    private void Start()
    {
        time = 0f;
        cooldownTimeAttack = 2f;
    }
    void Update()
    {
        time += Time.deltaTime;
        Vector3 direction = JoystickControl.direct.normalized;

        if (isDead == false)
        {
            if (time > cooldownTimeAttack)
            {
                isAttack = true;
                FindTarget();
            }
            else
            {
                isAttack = false;
            }
            if (direction != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(-direction);
                body.rotation = newRotation;
                body.Translate(body.forward * Time.deltaTime * speed, Space.World);
                ChangeAnim("run");
                isRunning = true;
            }
            else 
            {
                isRunning = false;
                ChangeAnim("idle");
            }
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("bot"))
            {
                if (Vector3.Distance(transform.position, collider.transform.position) < detectionRadius)
                {
                    collider.GetComponent<Bot>().inAreaAtack.SetActive(true);
                }
                else
                {
                    collider.GetComponent<Bot>().inAreaAtack.SetActive(false);
                }
            }
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        gameObject.tag = "player";
    }
    public override void OnDeath()
    {
        base.OnDeath();
        Invoke("DeActive", 2f);
    }
    public void FindTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("bot") && isAttack)
            {
                if (isRunning == false)
                {
                    anim.SetTrigger("attack");
                    target = collider.GetComponent<Character>().gameObject;
                    OnAttack();
                    time = 0;
                }
                break;
            }
        }
    }
    public override void SetNewPlayer()
    {
        base.SetNewPlayer();
        SetBaseStatAttack(0);
        SetBaseStatDefend(0);
        SetBaseStatHealth(0);
        initSkin.GetComponent<InitSkin>().PlayerEquipItem();
        UseWeapon();
    }
    private void DeActive()
    {
        gameObject.SetActive(false);
        UIManager.instance.DisplayLoseGameUI();
    }
    public override void SetBaseStatHealth(int health)
    {
        if (!PlayerPrefs.HasKey("Health"))
        {
            PlayerPrefs.SetInt("Health", BASE_HEALTH);
        }
        else
        {
            BASE_HEALTH += health;
            PlayerPrefs.SetInt("Health", BASE_HEALTH);
        }
    }
    public override void SetBaseStatAttack(int atk)
    {
        if (!PlayerPrefs.HasKey("Attack"))
        {
            PlayerPrefs.SetInt("Attack", BASE_ATTACK);
        }
        else
        {
            BASE_ATTACK += atk;
            PlayerPrefs.SetInt("Attack", BASE_ATTACK);
        }
    }
    public override void SetBaseStatDefend(int def)
    {
        if (!PlayerPrefs.HasKey("Defend"))
        {
            PlayerPrefs.SetInt("Defend", BASE_DEFEND);
        }
        else
        {
            BASE_DEFEND += def;
            PlayerPrefs.SetInt("Defend", BASE_DEFEND);
        }
    }
}
