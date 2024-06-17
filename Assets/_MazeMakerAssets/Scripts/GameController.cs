using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    [SerializeField] GameObject winPanel;
    [SerializeField] Button nextLevelButton;

    [SerializeField] GameObject lostPanel;
    [SerializeField] Button playAgainButton;

    [SerializeField] TextMeshProUGUI scoreText;
    private int score;

    private void Awake()
    {
        nextLevelButton.onClick.AddListener(LoadLevel);
        playAgainButton.onClick.AddListener(LoadLevel);
        score = 0;
        scoreText.text ="SCORE: " + score.ToString();
    
    }
    
    public void ChangeScore()
    {
        score++;
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void WinSequence()
    {
        winPanel.SetActive(true);
    }

    public void LostSequence()
    {
        lostPanel.SetActive(true);
    }

    public void LoadLevel()
    {
        Debug.Log("LoadLevel");
    }

}
