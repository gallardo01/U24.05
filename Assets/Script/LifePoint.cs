using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifePoint : MonoBehaviour
{
    public GameObject gameover;
    private int lifePoints = 3;
    public GameObject Controller;

    private void Start()
    {
        gameover.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            lifePoints--;
            if (lifePoints == 0)
            {
                gameover.SetActive(true);
                CheckGameover();
                Time.timeScale = 0f; // Stop the game
            }
        }
    }

    private void CheckGameover()
    {
        Controller.SetActive(false);
    }

    public void Reset()
    {
        // Reset the game here
        lifePoints = 3;
        gameover.SetActive(false);
        Controller.SetActive(true);
        Time.timeScale = 1f; // Resume the game

        // Reset the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
