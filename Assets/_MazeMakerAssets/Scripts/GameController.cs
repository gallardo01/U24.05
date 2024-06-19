using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int currentSceneIndex;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelButton.onClick.AddListener(LoadingNextScene);
        playAgainButton.onClick.AddListener(LoadingScene);
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

    public void LoadingNextScene()
    {
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void LoadingScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
