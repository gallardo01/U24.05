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
    public GameObject weaponEquipPos;
    public bool isAttack = false;
    public bool isRunning = false;
    public float cooldownTimeAttack = 2f;
    public float maxHP = 100;
    public float health;
    public string currentAnimName;
    public float detectionRadius = 25f;

    // Start is called before the first frame update
    public void Start()
    {
        health = maxHP;
        this.HPbar.GetComponent<HPbar>().SetHP();
        //EquipWeapon("boomerang");
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
        weapon.GetComponent<Rigidbody>().AddForce(body.forward * 1400f);
    }
    //void EquipWeapon(string weaponName)
    //{
    //    weaponPrefabs = instance.UseWeapon(weaponName);
    //    //weaponPrefabs.gameObject.tag = "chooseWeapon";
    //    GameObject weaponEquip = Instantiate(weaponPrefabs, weaponEquipPos.transform.position, Quaternion.Euler(90, 0, 0));
    //    weaponEquip.GetComponent<Weapon>().self = this;
    //    weaponEquip.transform.localScale = new Vector3(20,20,20);
    //    weaponEquip.GetComponent<Weapon>().enabled = false;
    //    weaponEquip.transform.SetParent(weaponEquipPos.transform);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("death"))
        {
            this.health = this.HPbar.GetComponent<HPbar>().ChangeHealth(-50);
            int randomIndex = Random.Range(0, GameController.instance.summonPoint.Count);
            transform.position = GameController.instance.summonPoint[randomIndex].position;
        }
        //if (GameController.instance.weaponTag.Contains(other.tag))
        //{
        //    EquipWeapon(other.tag);
        //    weaponPrefabs.GetComponent<Weapon>().enabled = true;
        //    GameController.instance.countWeaponSummon--;
        //}
    }
}
