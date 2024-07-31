using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Button playButton;
    [SerializeField] Button replayButton;
    [SerializeField] Button settingButton;
    


    private void OnEnable()
    {
        playButton.onClick.AddListener(() => SetGameState(GameState.GAME));
        replayButton.onClick.AddListener(() => LoadScene());
        settingButton.onClick.AddListener(() => SetGameState(GameState.SETTING));
    }

    private void Awake()
    {
        PlayersManager.Instance.OnInit();
    }

    private void Start()
    {
        SetGameState(GameState.MENU);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> listeners =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();

        foreach (var listener in listeners)
        {
            listener.OnGameStateChange(gameState);
        }
    }
}

public interface IGameStateListener
{
    void OnGameStateChange(GameState gameState);
}

public enum GameState
{
    MENU,
    GAME,
    SETTING,
    WEAPONSECTION,
    GAMEOVER,
    STAGECOMPLETE,
    SHOP,
}


