using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControlUI : MonoBehaviour, IGameStateListener
{
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] panels;
    [SerializeField] Color choseColor;

    private Dictionary<Button, GameObject> TabList = new Dictionary<Button, GameObject>();
    private List<Image> panelsImage = new List<Image>();

    private void OnInit()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            TabList.Add(buttons[i], panels[i]);
            panelsImage.Add(buttons[i].GetComponent<Image>());
        }

        foreach (KeyValuePair<Button, GameObject> kvp in TabList)
        {
            Button button = kvp.Key;
            button.onClick.AddListener(() => ShowPanel(kvp.Value));
        }
    }

    public void ShowPanel(GameObject panel)
    {

        for (int i = 0; i < panels.Length; i++)
        {
            if(panels[i] == panel)
            {
                panelsImage[i].color = choseColor;
                panels[i].SetActive(true);
            }
            else
            {
                panelsImage[i].color = Color.white;
                panels[i].SetActive(false);
            }
        }
        SoundManager.ButtonClick();
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.SHOP:
                if (TabList.Count == 0) OnInit();
                ShowPanel(panels[0]);
                break;
        }
    }
}
