using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject weaponSelecPanel;
  
    List<GameObject> panels = new List<GameObject>();

    private void Awake()
    {
        panels.AddRange( new GameObject[] { menuPanel ,gamePanel, settingPanel, gameOverPanel, shopPanel, weaponSelecPanel,   } );
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);

                break;
            case GameState.GAME:
                ShowPanel(gamePanel);

                break;
            case GameState.WEAPONSECTION:
                ShowPanel(weaponSelecPanel);

                break;
            case GameState.SETTING:
                ShowPanel(settingPanel);

                break;
            case GameState.GAMEOVER:
                ShowPanel(gameOverPanel);

                break;
            case GameState.SHOP:
                ShowPanel(shopPanel);

                break;
        }
    }

    public void ShowPanel(GameObject panel)
    {
        for(int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(panels[i] == panel);
        }
    }
}
