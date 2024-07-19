using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] int characterLayer;
    [SerializeField] Character owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == characterLayer)
        {
            owner.AddTarget(Cache.Ins.GetCachedComponent<Character>(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == characterLayer)
        {
            owner.RemoveTarget(Cache.Ins.GetCachedComponent<Character>(other));
        }
    }
}
