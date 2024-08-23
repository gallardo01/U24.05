using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    [SerializeField] Player player;
    public TMPro.TextMeshProUGUI attackPlayer;
    public TMPro.TextMeshProUGUI defendPlayer;
    public TMPro.TextMeshProUGUI healthPlayer;

    public void DisplayStats()
    {
        attackPlayer.text = player.attack.ToString();
        defendPlayer.text = player.defend.ToString();
        healthPlayer.text = player.health.ToString();
    }
}
