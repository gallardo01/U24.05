using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameController;

public class Character : MonoBehaviour
{
    [SerializeField] public Transform body;
    [SerializeField] public Animator anim;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject HPbar;
    [SerializeField] TMP_Text namePlayer;
    [SerializeField] Image image;
    [SerializeField] TMP_Text levelPlayer;

    public GameObject weaponPrefabs;
    public GameObject weaponEquipPos;
    public bool isAttack = false;
    public bool isRunning = false;
    public float cooldownTimeAttack = 1.5f;
    public float maxHP = 100;
    public float health;
    public string currentAnimName;
    public float detectionRadius = 15f;

    public void Start()
    {
        health = maxHP;
        this.HPbar.GetComponent<TargetIndicator>().SetHP();
        weaponPrefabs = GameController.instance.UseWeapon("boomerang");
        namePlayer.text = RandomNameGenerator.GenerateRandomName();
        namePlayer.color = Random.ColorHSV();
        image.color = namePlayer.color;
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

    public void FireWeapon(GameObject weaponPrefabs, GameObject target)
    {
        Vector3 directionToTarget = (target.GetComponent<Character>().transform.position - transform.position).normalized;
        body.rotation = Quaternion.LookRotation(directionToTarget);
        GameObject weapon = Instantiate(weaponPrefabs, firePoint.position, Quaternion.Euler(90, 0, 0));
        weapon.GetComponent<Weapon>().self = this;
        weapon.GetComponent<Rigidbody>().AddForce(body.forward * 900f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("death"))
        {
            this.health = this.HPbar.GetComponent<TargetIndicator>().ChangeHealth(-50);
            int randomIndex = Random.Range(0, GameController.instance.summonPoint.Count);
            transform.position = GameController.instance.summonPoint[randomIndex].position;
        }

    }
}
