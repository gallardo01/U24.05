using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SCR_Menu01 : MonoBehaviour
{
    [SerializeField] Button m_SubmitBTN;
    [SerializeField] Button m_AscendingBTN;
    [SerializeField] Button m_DescendingBTN;
    [SerializeField] Button m_DeleteBTN;
    [SerializeField] TMP_InputField m_InputINP;
    [SerializeField] TextMeshProUGUI m_ArrayInfoTXT;
    [SerializeField] TextMeshProUGUI m_ListInfoTXT;
    [SerializeField] TextMeshProUGUI m_DictInfoTXT;

    private int[] array = new int[100];
    private List<int> list = new List<int>();
    private Dictionary<string, int> dict = new Dictionary<string, int>();

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_SubmitBTN.onClick.AddListener(delegate { Submit(); });
        m_AscendingBTN.onClick.AddListener(delegate { Sort(); });
        m_DescendingBTN.onClick.AddListener(delegate { Sort(false); });
        m_DeleteBTN.onClick.AddListener(delegate { Delete(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Submit()
    {
        Debug.Log($"Click on submit button");
        string inputText = m_InputINP.text;
        
        if(inputText != "")
        {
            if (int.TryParse(inputText, out int result))
            {
                if (index < array.Length)
                {
                    array[index] = result;
                    index++;
                }
                list.Add(result);
                dict.Add("x" + index, result);
            }
            UpdateUI();
        }
    }    

    public void UpdateUIArrayInfo(int[] array)
    {

    }   
    
    public void UpdateUI()
    {
        string info = "";
        if(index == 0)
        {
            m_ArrayInfoTXT.text = "";
        }
        else
        {
            for (int i = 0; i < index; i++)
            {
                info += array[i] + " ";
            }
            m_ArrayInfoTXT.text = info;
        }

        info = "";
        if(list.Count == 0)
        {
            m_ListInfoTXT.text = "";
        }
        else
        {
            foreach (var i in list)
            {
                info += i + " ";
            }
            m_ListInfoTXT.text = info;
        }
        

        info = "";
        if(dict.Count == 0)
        {
            m_DictInfoTXT.text = "";
        }
        else
        {
            foreach (var item in dict)
            {
                info += "{" + item.Key + ": " + item.Value + "} ";
            }
            m_DictInfoTXT.text = info;
        }
    }
    
    public void Sort(bool isAscending = true)
    {
        List<int> ls = array.ToList();
        ls.RemoveAll(x => x == 0);
        index = ls.Count;
        if(isAscending)
        {
            list.Sort();
            ls.Sort();
            dict = dict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        else
        {
            list.Sort((a,b) => b.CompareTo(a));
            ls.Sort((a,b) => b.CompareTo(a));
           dict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        array = ls.ToArray();

        UpdateUI();
    }

    public void Delete()
    {
        string inputText = m_InputINP.text;
        if (inputText != "" )
        {
            if (int.TryParse(inputText, out int result))
            {
                //array
                List<int> ls = array.ToList();
                index -= ls.RemoveAll(x => x == result);
                array = ls.ToArray();
                
                //list
                list.RemoveAll(x => x == result);

                //dict 
                List<string> keyToRemove = dict.Where(x => x.Value == result).Select(x => x.Key).ToList();
                foreach(var item in keyToRemove)
                {
                    dict.Remove(item);
                }    
                UpdateUI();
            }
        }
    }
}
