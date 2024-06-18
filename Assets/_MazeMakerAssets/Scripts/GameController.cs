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

    [SerializeField] GameObject ChestOpen;
    [SerializeField] GameObject ChestClose;
    [SerializeField] ParticleSystem celebration1;
    [SerializeField] ParticleSystem celebration2;
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
        ChestOpen.SetActive(false);
        ChestClose.SetActive(true);
        celebration1.Play();
        celebration2.Play();
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
