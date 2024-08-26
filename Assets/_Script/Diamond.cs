using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.diamondPoint++;
            player.diamondText.text = player.diamondPoint.ToString();
            Destroy(gameObject);
        }
    }
}
