using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GameController;
using static SoundManager;
using static UnityEngine.GraphicsBuffer;
using Image = UnityEngine.UI.Image;

public class Character : AbstractCharacter
{
    [SerializeField] public Transform body;
    [SerializeField] public Animator anim;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject HPbar;
    [SerializeField] public TMP_Text namePlayer;
    [SerializeField] Image image;
    [SerializeField] TMP_Text levelPlayer;

    public InitSkin initSkin;
    public GameObject weaponPrefabs;
    public GameObject target;
    public bool isAttack = false;
    public bool isRunning = false;
    public bool isDead;
    public float cooldownTimeAttack;

    public float health;
    public string currentAnimName;
    public float detectionRadius = 15f;
    public int level = 1;
    public int attack;
    public int defend;

    public int BASE_ATTACK = 20;
    public int BASE_DEFEND = 5;
    public int BASE_HEALTH = 100;

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
        CreatDataPlayer();
    }
    public override void OnAttack()
    {
        FireWeapon();
    }
    public override void OnDeath()
    {
        CharacterDeath();
    }

    public GameObject UseWeapon()
    {
        string weaponName = initSkin.weaponEquiped.name;

        foreach (GameObject weapon in GameController.instance.weaponList)
        {
            if (weaponName == weapon.name)
            {
                weaponPrefabs = weapon;
                return weaponPrefabs;
            }
        }
        return null;
    }
    private void CreatDataPlayer()
    {
        namePlayer.enabled = true;
        namePlayer.text = RandomNameGenerator.GenerateRandomName();
        namePlayer.color = Random.ColorHSV();
        image.color = namePlayer.color;
        isDead = false;
        LevelUpData();
    }
    public virtual void SetNewPlayer()
    {
        isDead = true;
        health = BASE_HEALTH;
        attack = BASE_ATTACK;
        defend = BASE_DEFEND;
        namePlayer.enabled = false;
        level = 0;
        LevelUpData();
        this.HPbar.GetComponent<TargetIndicator>().SetHP();
    }
    public void FireWeapon()
    {
        Vector3 directionToTarget = (target.GetComponent<Character>().transform.position - transform.position).normalized;
        body.rotation = Quaternion.LookRotation(directionToTarget);
        GameObject weapon = EasyObjectPool.instance.GetObjectFromPool(weaponPrefabs.name, firePoint.position, Quaternion.Euler(90, 0, 0));
        weapon.GetComponent<Weapon>().self = this;
        weapon.GetComponent<Weapon>().SetTimeDisappear();
        weapon.GetComponent<Rigidbody>().AddForce(body.forward * 900f);
        SoundManager.instance.PlayOneShot(SoundList.Shot);
    }
    public void SetBodyScale(int level)
    {
        body.transform.localScale = Vector3.one * (0.05f * (level-1) + 0.5f);
    }

    private void SetDetectionRadius(int level) 
    {         
        detectionRadius = 15f + (level * 1.8f);
    }

    public void LevelUpPlayer()
    {
        level++;
    }
    public virtual void LevelUpData()
    {
        SetBodyScale(level);
        SetDetectionRadius(level);
        levelPlayer.text = level.ToString();
    }
    public void CharacterDeath()
    {
        isDead = true;
        ChangeAnim("dead");
        if (this.GetComponent<Bot>())
        {
            GameController.instance.countBots.Remove(gameObject);
        }       
        this.tag = "Untagged";
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
    public virtual void SetBaseStatHealth(int health)
    {

    }
    public virtual void SetBaseStatAttack(int atk)
    {

    }
    public virtual void SetBaseStatDefend(int def)
    {

    }
}
