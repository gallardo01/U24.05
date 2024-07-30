using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameController;
using static UnityEngine.GraphicsBuffer;

public class Character : AbstractCharacter
{
    [SerializeField] public Transform body;
    [SerializeField] public Animator anim;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject HPbar;
    [SerializeField] public TMP_Text namePlayer;
    [SerializeField] Image image;
    [SerializeField] TMP_Text levelPlayer;

    public GameObject weaponPrefabs;
    public GameObject target;
    public GameObject weaponEquipPos;
    public bool isAttack = false;
    public bool isRunning = false;
    public bool isDead;
    public float cooldownTimeAttack = 1.5f;
    public float maxHP = 100;
    public float health;
    public string currentAnimName;
    public float detectionRadius = 15f;
    public int level = 1;

    public void Start()
    {
        namePlayer.enabled = false;
        isDead = true;
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public override void OnInit()
    {
        health = maxHP;
        this.HPbar.GetComponent<TargetIndicator>().SetHP();
        weaponPrefabs = GameController.instance.UseWeapon("boomerang");
        namePlayer.enabled = true;
        namePlayer.text = RandomNameGenerator.GenerateRandomName();
        namePlayer.color = Random.ColorHSV();
        image.color = namePlayer.color;
    }
    public override void OnAttack()
    {
        FireWeapon();
    }
    public override void OnDeath()
    {
        CharacterDeath();
    }

    public void FireWeapon()
    {
        Vector3 directionToTarget = (target.GetComponent<Character>().transform.position - transform.position).normalized;
        body.rotation = Quaternion.LookRotation(directionToTarget);
        GameObject weapon = Instantiate(weaponPrefabs, firePoint.position, Quaternion.Euler(90, 0, 0));
        weapon.GetComponent<Weapon>().self = this;
        weapon.GetComponent<Rigidbody>().AddForce(body.forward * 900f);
    }

    public void SetBodyScale(int level)
    {
        body.transform.localScale = Vector3.one * (0.1f * (level-1) + 0.5f);
    }

    private void SetDetectionRadius(int level) 
    {         
        detectionRadius = 15f + (level * 2f);
    }
        
    public void LevelUp()
    {
        level++;
        SetBodyScale(level);
        SetDetectionRadius(level);
        levelPlayer.text = level.ToString();
    }
    public void CharacterDeath()
    {
        isDead = true;
        ChangeAnim("dead");
        GameController.instance.countPlayers.Remove(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("death"))
        {
            if (health>50)
            {
                this.health = this.HPbar.GetComponent<TargetIndicator>().ChangeHealth(-50);
                int randomIndex = Random.Range(0, GameController.instance.summonPoint.Count);
                transform.position = GameController.instance.summonPoint[randomIndex].position;
            } else
            {
                OnDeath();
            }   
        }
    }
}
