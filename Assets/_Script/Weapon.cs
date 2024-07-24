using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Character self;
    float timeDissappear = 1f;
    float time;
    int playerAlive;
    private void Start()
    {
        playerAlive = GameController.instance.countPlayer;
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.Rotate(Vector3.forward * 250f * Time.deltaTime);
        if (time > timeDissappear)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Bot bot = other.GetComponent<Bot>();
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("bot") && other.GetComponent<Character>() != self)
        {
            Destroy(gameObject);
            bot.health = bot.HPbar.GetComponent<TargetIndicator>().ChangeHealth(-40);        
            if (bot.health < 0)
            {
                bot.ChangeAnim("dead");
                playerAlive--;
                GameController.instance.CountPlayer(playerAlive);
                Destroy(other.gameObject,1.5f);
            }
        } else if (other.CompareTag("player") && other.GetComponent<Character>() != self)
        {
            Destroy(gameObject);
            player.health = player.HPbar.GetComponent<TargetIndicator>().ChangeHealth(-40);
            if (player.health < 0)
            {
                player.ChangeAnim("dead");
                playerAlive--;
                GameController.instance.CountPlayer(playerAlive);
                Destroy(other.gameObject,1.5f);   
            }
        }
    }
}
