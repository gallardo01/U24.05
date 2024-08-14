using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControlUI : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] panels;
    [SerializeField] Color choseColor;

    private Dictionary<Button, GameObject> TabList = new Dictionary<Button, GameObject>();
    List<Image> panelsImage = new List<Image>();

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            TabList.Add(buttons[i], panels[i]);
            panelsImage.Add(buttons[i].GetComponent<Image>());
        }

        foreach(KeyValuePair<Button, GameObject> kvp in TabList)
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
        
    }
}
