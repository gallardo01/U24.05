using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    private int[] numbers = new int[10];
    private int count_array = 0;

    private List<int> list_numbers = new List<int>();
    private Dictionary<int, int> dic_numbers = new Dictionary<int, int>();
    private int count_dic = 0;

    public TextMeshProUGUI array_contents;
    public TextMeshProUGUI list_contents;
    public TextMeshProUGUI dic_contents;

    public TMP_InputField inputField;
    public Button inputButton;
    public Button sortButton;


    void Start()
    {
        InitDataIntoText();
        inputButton.onClick.AddListener(() => PressButtonInput());
        sortButton.onClick.AddListener(() => AsscendingSort());

    }

    private void AsscendingSort()
    {
        // sort


        InitDataIntoText();
    }

    private void PressButtonInput()
    {
        // Array
        numbers[count_array] = Int32.Parse(inputField.text);
        count_array++;
        // List
        list_numbers.Add(Int32.Parse(inputField.text));
        // Dictionary
        dic_numbers.Add(count_dic, Int32.Parse(inputField.text));
        count_dic++;

        inputField.text = "";
        InitDataIntoText();
    }

    private void InitDataIntoText()
    {
        // Array
        string textArray = "";
        for (int i = 0; i < numbers.Length; i++)
        {
            textArray += numbers[i] + ", ";
        }
        array_contents.text = textArray;

        // List
        string textArrayList = "";
        for (int i = 0; i < list_numbers.Count; i++)
        {
            textArrayList += list_numbers[i] + ", ";
        }
        list_contents.text = textArrayList;

        // Dictionary
        string textDictionary = "";
        foreach (KeyValuePair<int, int> number in dic_numbers)
        {
            textDictionary += number.Value + ", ";
        }
        dic_contents.text = textDictionary;
    }

    
}
