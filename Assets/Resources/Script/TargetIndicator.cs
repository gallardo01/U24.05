using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetIndicator : MonoBehaviour
{
    private Character character;
    public Image colorImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitTargetIndicator(Color color, string name, int level)
    {
        colorImage.color = color;
        nameText.text = name;
        levelText.text = level.ToString();
    }
    
    public void InitTargetIndicator  (int level)
    {
        levelText.text = level.ToString();
    }
}
