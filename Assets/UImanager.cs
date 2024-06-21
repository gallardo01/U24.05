using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject stage;
    [SerializeField] GameObject shop;
    [SerializeField] GameObject back;
    [SerializeField] List<GameObject> stageList;

    private void Start()
    {
        if (true)
        {
            
        }
    }
    public void OpenStageMenu()
    {
        menu.SetActive(false);
        stage.SetActive(true);
        shop.SetActive(false);
    }
    public void OpenShopMenu()
    {
        menu.SetActive(false);
        stage.SetActive(false);
        shop.SetActive(true);
    }

    public void BackToMenu()
    {
        menu.SetActive(true);
        stage.SetActive(false);
        shop.SetActive(false);
    }
}
