using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifePoint : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject controllerGame;
    public TMP_Text lifePointText;
    bool isGameOver = false;
    public int lifePoint = 3;
    private void Start()
    {
        DisplayLifePoint(lifePoint);
        gameOver.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("monster"))
        {
            lifePoint -= 1;
            if (lifePoint == 0)
            {
                gameOver.SetActive(true);
                isGameOver = true;
                CheckGameOver(isGameOver);
            }
        }
        DisplayLifePoint(lifePoint);
    }
    private void DisplayLifePoint(int lifePoint)
    {
        lifePointText.text = lifePoint.ToString();
    }
    public void CheckGameOver(bool isGameOver)
    {
        if (isGameOver)
        {
            controllerGame.SetActive(false);
        } else
        {
            lifePoint = 3;
            controllerGame.SetActive(true);
        }
    }
    public void RetryNewGame()
    {
        Debug.Log(1);
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
