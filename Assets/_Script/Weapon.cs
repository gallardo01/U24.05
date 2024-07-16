using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float timeDissappear = 1f;
    float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.Rotate(Vector3.forward * 170f * Time.deltaTime);
        if (time > timeDissappear)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bot"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
