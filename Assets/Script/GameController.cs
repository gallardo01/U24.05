using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

public class GameController : MonoBehaviour
{

    private int[] numbers = new int[10];
    private int count_array = 0;
    private List<int> list = new List<int>();
    private Dictionary<string, int> dictionary = new Dictionary<string, int>();
    //public int[] numbers_public = new int[10];
    // Start is called before the first frame update

    public TextMeshProUGUI array_contents;
    public TextMeshProUGUI list_contents;
    public TextMeshProUGUI dictionary_contents;

    public TMP_InputField inputField;
    public Button inputButton;
    public Button sortUpButton;
    public Button sortDownButton;
    public Button deleteButton; // Added delete button

    void Start()
    {
        InitDataIntoText();
        inputButton.onClick.AddListener(() => PressButtonInput());
        sortUpButton.onClick.AddListener(() => SortUpAscending());
        sortDownButton.onClick.AddListener(() => SortDownAscending());
        deleteButton.onClick.AddListener(() => DeleteValue()); // Added delete button listener
    }

    private void PressButtonInput()
    {
        // Arrray
        numbers[count_array] = int.Parse(inputField.text);
        InitDataIntoText();
        count_array++;

        // List
        list.Add(int.Parse(inputField.text));
        InitDataIntoText();


        // Dictionary
        string key = "Key" + dictionary.Count;
        dictionary.Add(key, int.Parse(inputField.text));
        InitDataIntoText();
    }

    private void SortUpAscending()
    {
        // Sort array
        numbers = numbers.OrderBy(x => x).ToArray();
        InitDataIntoText();

        // Sort list
        list.Sort();
        InitDataIntoText();

        // Sort dictionary
        dictionary = dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        InitDataIntoText();
    }

    private void SortDownAscending()
    {
        // Sort array reverser
        numbers = numbers.OrderByDescending(x => x).ToArray();
        InitDataIntoText();

        // Sort list reverser
        list.Sort();
        list.Reverse();
        InitDataIntoText();

        // Sort dictionary reverser
        dictionary = dictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        InitDataIntoText();
    }

    //private void DeleteInputValue()
    //{
    //    // Remove value from array
    //    if (count_array > 0)
    //    {
    //        count_array--;
    //        numbers[count_array] = 0;
    //        InitDataIntoText();
    //    }

    //    // Remove value from list
    //    if (list.Count > 0)
    //    {
    //        list.RemoveAt(list.Count - 1);
    //        InitDataIntoText();
    //    }

    //    // Remove value from dictionary
    //    if (dictionary.Count > 0)
    //    {
    //        string lastKey = "Key" + (dictionary.Count - 1);
    //        dictionary.Remove(lastKey);
    //        InitDataIntoText();
    //    }
    //}

    private void DeleteValue()
    {
        int inputNumber = int.Parse(inputField.text);

        if (numbers.Contains(inputNumber))
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == inputNumber)
                {
                    numbers[i] = 0;
                }
            }
        }

        if (list.Contains(inputNumber))
        {
            list.RemoveAll(x => x == inputNumber);
        }

        if (dictionary.ContainsValue(inputNumber))
        {
            var keysToRemove = dictionary.Where(x => x.Value == inputNumber).Select(x => x.Key).ToList();
            foreach (var key in keysToRemove)
            {
                dictionary.Remove(key);
            }
        }

        InitDataIntoText();
    }

    private void InitDataIntoText()
    {
        string textArray = "";
        string textList = "";
        string textDictionary = "";

        for (int i = 0; i < numbers.Length; i++)
        {
            // Array
            textArray += numbers[i] + " , ";
        }

        for (int i = 0; i < list.Count; i++)
        {
            // List
            textList += list[i] + " , ";
        }

        foreach (KeyValuePair<string, int> item in dictionary)
        {
            // Dictionary
            textDictionary += item.Key + " : " + item.Value + " , ";
        }
        array_contents.text = textArray;
        list_contents.text = textList;
        dictionary_contents.text = textDictionary;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
