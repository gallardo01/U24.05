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
    public TextMeshProUGUI array_contents;
    public TMP_InputField inputField;
    public Button inputButton;

    void Start()
    {
        InitDataIntoText();
        inputButton.onClick.AddListener(() => PressButtonInput());
    }

    private void PressButtonInput()
    {
        // Array
        numbers[count_array] = Int32.Parse(inputField.text);
        count_array++;
        InitDataIntoText();
        // List


        // Dictionary
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
    }

    
}
