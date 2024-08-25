using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour, IGameStateListener
{
    [Header("UI")]
    [SerializeField] RectTransform content;
    [SerializeField] HorizontalLayoutGroup horizontal;
    [SerializeField] float itemSize;
    [SerializeField] MapContainer[] containers;

    private Vector2 startPos;
    private int itemCount;
    private float offsetRight;
    private float offsetLeft;

    [Header("Logic")]
    [SerializeField] MapDataSO MapDataSO;
    private MapData[] Datas;
    private List<Button> Buttons = new List<Button>();

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MAPSELECTION:
                Configue();
                break;
        }
    }

    private void Configue()
    {
        if(Datas == null) { Datas = MapDataSO.mapDatas; }

        for(int i = 0; i < containers.Length; i++)
        {
            MapData newMap = Datas[i];
            MapContainer container = containers[i];
            container.Configue(newMap);
            container.Button.onClick.AddListener(() => LoadMap(container));
        }
    }

    private void LoadMap(MapContainer container)
    {
        Instantiate(container.Data.prefab);
        GameManager.Instance.SetGameState(GameState.GAME);
    }

    void Update()
    {
        Debug.Log(Time.time);
        if(content.anchoredPosition.x > 52)
        {
            content.anchoredPosition = Vector2.right * -3281;
        }
        if (content.anchoredPosition.x < -3281)
        {
            content.anchoredPosition = Vector2.right * 52;
        }
    }

}
