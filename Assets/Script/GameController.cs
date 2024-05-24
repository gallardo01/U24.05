using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Xml;
using UnityEditor.UIElements;

public class GameController : MonoBehaviour
{
    private int[] array_numbers = new int[10];
    private List<int> list_numbers = new List<int>(10);
    private Dictionary<int,string> dictionary_numbers = new Dictionary<int, string>();
    private int count_numbers = 0;

    //GameObject array_contents; => Them doi tuong game object
    public TextMeshProUGUI array_contents;
    public TextMeshProUGUI list_contents;
    public TextMeshProUGUI dictionary_contents;
    public TMP_InputField inputField;
    public Button inputButton;
    public Button increaseButton;
    public Button decreaseButton;
    public Button clearButton;

    // Start is called before the first frame update
    void Start()
    {
        InitDataIntoText();
        inputButton.onClick.AddListener(() => PressButtonInput());
        increaseButton.onClick.AddListener(() => PressButtonIncrease());
        decreaseButton.onClick.AddListener(() => PressButtonDecrease());
        clearButton.onClick.AddListener(() => PressButtonClear());
    }

    private void PressButtonClear()
    {
        int numberClear = Int32.Parse(inputField.text);
        //Array
        for (int i = 0; i < array_numbers.Length-1; i++)
        {
            if (numberClear == array_numbers[i])
            {
                array_numbers[i] = 0;
            }
        }
        //List
        for (int i = 0; i < list_numbers.Count -1; i++)
        {
            list_numbers.RemoveAll(x => x == numberClear);
        }

        List<int> deleteKey = new List<int>();
        //Dictionary
        foreach (var item in dictionary_numbers)
        {
            if (item.Value == numberClear.ToString())
            {
                deleteKey.Add(item.Key);
            }
        }

        // => Làm tiếp vòng for

            //Cách 2:
            //if (dictionary_numbers.ContainsValue(numberClear.ToString()))
            //{
            //    var keysToRemove = dictionary_numbers.Where(x => x.Value == numberClear.ToString()).Select(x => x.Key).ToList();
            //    foreach (var key in keysToRemove)
            //    {
            //        dictionary_numbers.Remove(key);
            //    }
            //}
            InitDataIntoText();
        Debug.Log("Da xoa");
    }

    private void PressButtonDecrease()
    {
        //Array
        int[] array_SortNumbers = array_numbers;
        array_numbers = array_numbers.OrderByDescending(x => x).ToArray();
        //List
        List<int> list_SortNumbers = list_numbers;
        list_SortNumbers.Sort((a,b) => b-a);
        //Dictionary
        dictionary_numbers = dictionary_numbers.OrderByDescending( x=> x.Value).ToArray().ToDictionary(x =>x.Key,x =>x.Value);
        InitDataIntoText();
    }

    private void PressButtonIncrease()
    {
        //Array
        int[] array_SortNumbers = array_numbers;
        array_numbers = array_numbers.OrderBy( x=> x).ToArray();
        //List
        List<int> list_SortNumbers = list_numbers;
        list_SortNumbers.Sort();
        //Dictionary
        //var dictionary_SortNumbers = dictionary_numbers.OrderBy(pair => pair.Value); => Lỗi
        dictionary_numbers = dictionary_numbers.OrderBy( x=> x.Value).ToArray().ToDictionary(x =>x.Key,x =>x.Value);
        InitDataIntoText();
    }

    private void PressButtonInput()
    {
        //Array
        array_numbers[count_numbers] = Int32.Parse(inputField.text);
        count_numbers++;
        //List
        list_numbers.Add(Int32.Parse(inputField.text));
        //Dictionary
        dictionary_numbers.Add(count_numbers, inputField.text);

        inputField.text = "";
        InitDataIntoText();
    }
    private void InitDataIntoText()
    {
        //Array
        string textArray = "";
        for (int i = 0; i < array_numbers.Length; i++)
        {
            textArray += ", " + array_numbers[i];
        }
        array_contents.text = textArray;
        //List
        string textList = "";
        for (int i = 0; i < list_numbers.Count; i++)
        {
            textList += ", " + list_numbers[i];
        }
        list_contents.text = textList;
        //Dictionary
        string textDictionary = "";
        foreach (var item in dictionary_numbers)
        {
            textDictionary += ", " + item.Key + " : " + item.Value;
        }
        dictionary_contents.text = textDictionary;
    }
}
