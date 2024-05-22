using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Input : MonoBehaviour
{
    public Text inputText;
    public Text outputText;
    public Button addButton;
    public Button sortButton;
    public Button deleteButton;

    private int[] array = new int[10];
    private List<int> list = new List<int>();
    private Dictionary<string, int> dictionary = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        addButton.onClick.AddListener(AddValues);
        sortButton.onClick.AddListener(SortValues);
        deleteButton.onClick.AddListener(DeleteValues);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddValues()
    {
        int value = int.Parse(inputText.text);

        // Add to array
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 0)
            {
                array[i] = value;
                break;
            }
        }

        // Add to list
        list.Add(value);

        // Add to dictionary
        string key = "Key" + dictionary.Count;
        dictionary.Add(key, value);

        // Update output text
        UpdateOutputText();
    }

    void SortValues()
    {
        // Sort array
        System.Array.Sort(array);

        // Sort list
        list.Sort();

        // Sort dictionary by value
        var sortedDictionary = dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        dictionary = sortedDictionary;

        // Update output text
        UpdateOutputText();
    }

    void DeleteValues()
    {
        // Clear array
        System.Array.Clear(array, 0, array.Length);

        // Clear list
        list.Clear();

        // Clear dictionary
        dictionary.Clear();

        // Update output text
        UpdateOutputText();
    }

    void UpdateOutputText()
    {
        string output = "Array: ";
        for (int i = 0; i < array.Length; i++)
        {
            output += array[i] + " ";
        }

        output += "\nList: ";
        for (int i = 0; i < list.Count; i++)
        {
            output += list[i] + " ";
        }

        output += "\nDictionary: ";
        foreach (var kvp in dictionary)
        {
            output += kvp.Key + ": " + kvp.Value + " ";
        }

        outputText.text = output;
    }
}
