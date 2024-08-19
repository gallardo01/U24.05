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
    [SerializeField] Button shopButton;
    [SerializeField] Button shopBackButton;
    [SerializeField] Button settingToMenu;
    [SerializeField] Button settingToGame;
    
    public static int RoundPoint { get; private set; }

    private void OnEnable()
    {
        playButton.onClick.AddListener(() => SetGameState(GameState.GAME));
        replayButton.onClick.AddListener(() => LoadScene());
        settingButton.onClick.AddListener(() => SetGameState(GameState.SETTING));
        shopButton.onClick.AddListener(() => SetGameState(GameState.SHOP));
        shopBackButton.onClick.AddListener(() => SetGameState(GameState.MENU));
        settingToMenu.onClick.AddListener(() => SetGameState(GameState.MENU));
        settingToGame.onClick.AddListener(() => SetGameState(GameState.GAME));
    }

    private void Awake()
    {
        CharactersManager.Instance.OnInit();
    }

    private void Start()
    {
        SetGameState(GameState.MENU);

        //string[] keys = ES3.GetKeys();
        //if (keys.Length == 0)
        //{
        //    Debug.Log("No keys found in the save file.");
        //}
        //else
        //{
        //    Debug.Log("Keys found in the save file:");
        //    foreach (string key in keys)
        //    {
        //        Debug.Log(key);
        //    }
        //}
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                CurrencyManager.Instance.AddCurrency(500);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ES3.DeleteFile();
            }
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                CurrencyManager.Instance.ResetCurrency();
            }
        }
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2f);

    }

    public static void AddPoint(int point)
    {
        RoundPoint += point;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        SetGameState(GameState.GAMEOVER);
    }

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> listeners =
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.None).OfType<IGameStateListener>();

        foreach (var listener in listeners)
        {
            listener.OnGameStateChange(gameState);
        }
        SoundManager.ButtonClick();
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


