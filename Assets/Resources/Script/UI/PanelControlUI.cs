using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControlUI : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] panels;

    private Dictionary<Button, GameObject> TabList = new Dictionary<Button, GameObject>();

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            TabList.Add(buttons[i], panels[i]);
        }

        foreach(KeyValuePair<Button, GameObject> kvp in TabList)
        {
            kvp.Key.onClick.AddListener(() => ShowPanel(kvp.Value));
        }
    }

    public void ShowPanel(GameObject panel)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(panels[i] == panel);
        }
    }
}
