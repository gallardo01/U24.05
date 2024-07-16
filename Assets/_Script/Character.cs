using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public Transform body;
    [SerializeField] public Animator anim;
    [SerializeField] public GameObject weaponPrefabs;
    [SerializeField] public Transform firePoint;
    public string currentAnimName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void FireWeapon()
    {
        ChangeAnim("attack");
        GameObject weapon = Instantiate(weaponPrefabs, firePoint.position, Quaternion.Euler(90, 0, 0));
        weapon.GetComponent<Rigidbody>().AddForce(body.forward * 900f);
    }
}
