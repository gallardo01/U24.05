using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //private int[] numbers_private = new int[10];
    public int[] numbers = new int[10];

    public TextMeshProUGUI array_contents;
    public TextMeshProUGUI dic_contents;
    public TextMeshProUGUI list_contents;
    public TMP_InputField inputField;
    public Button inputButton;
    private int count_arr = 0;
    public List<int> list = new List<int>();
    public Dictionary<string, string> dictionary = new Dictionary<string, string>(5);
    public int countKey=0;

    void Start()
    {
        InitData();
        inputButton.onClick.AddListener(() => PressButton());
        
    }

    private void PressButton()
    {
        if (int.TryParse(inputField.text, out int result))
        {
            numbers[count_arr] = result;
            count_arr++;

            list.Add(result);

            dictionary.Add(countKey + "", inputField.text);
            countKey++;
            InitData();

            inputField.text = "";
        }

        
        //Debug.Log(ress");
       
        

    }

    private void InitData()
    {
        string textList = "";
        string textArray = "";
        string textDic = "";
        for (int i = 0; i < numbers.Length; i++)
        {
            //textList += numbers[i]+",";
            textArray += numbers[i]+",";
        }
        array_contents.text = textArray;
        for(int i = 0;i < list.Count; i++)
        {
            textList += list[i]+",";
        }
        list_contents.text = textList;
       foreach(var x in dictionary) 
        {
            textDic += x.Value+",";
       
        }
        dic_contents.text = textDic;
    }
    // Update is called once per frame
    void Update()
    {
        


    }
}
