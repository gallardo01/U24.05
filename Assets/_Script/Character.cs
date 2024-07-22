using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

public class Character : MonoBehaviour
{
    [SerializeField] public Transform body;
    [SerializeField] public Animator anim;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject HPbar;

    public GameObject weaponPrefabs;
    public bool isAttack = false;
    public float cooldownTimeAttack = 2f;
    public float maxHP = 100;
    public float health;
    public string currentAnimName;
    public float detectionRadius = 25f;

    // Start is called before the first frame update
    void OnEnable()
    {
        health = maxHP;
        weaponPrefabs = instance.UseWeapon(WeaponName.boomerang);
        this.HPbar.GetComponent<HPbar>().SetHP();
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

    public void FireWeapon(GameObject weaponPrefabs)
    {
        ChangeAnim("attack");
        GameObject weapon = Instantiate(weaponPrefabs, firePoint.position, Quaternion.Euler(90, 0, 0));
        weapon.GetComponent<Rigidbody>().AddForce(body.forward * 1400f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("death"))
        {
            this.health = this.HPbar.GetComponent<HPbar>().ChangeHealth(-50);
            int randomIndex = Random.Range(0, GameController.instance.summonPoint.Count);
            transform.position = GameController.instance.summonPoint[randomIndex].position;
        }
    }
}
