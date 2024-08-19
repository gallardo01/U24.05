using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TargetIndicator : MonoBehaviour
{
    public Character character;
    public Image colorImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nameText;
    public Camera Camera => CameraFollower.Ins.gameCamera;
    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;
    private float indicatorY = 4f;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void LateUpdate()
    {
        // viewPoint = Camera.WorldToViewportPoint(character.transform.position + Vector3.forward * indicatorY);
        // GetComponent<RectTransform>().anchoredPosition = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
        
        viewPoint = Camera.WorldToViewportPoint(character.indicatorPoint.transform.position);
        //viewPoint = Camera.WorldToViewportPoint(character.transform.position);

        viewPoint.x = Mathf.Clamp(viewPoint.x, 0.075f, 0.925f);
        viewPoint.y = Mathf.Clamp(viewPoint.y, 0.075f, 0.875f);
        GetComponent<RectTransform>().anchoredPosition = Camera.ViewportToScreenPoint(viewPoint) - screenHalf;
    }

    
    public void InitTarget(Color color, int level, string name)
    {
        colorImage.color = color;
        levelText.text = level.ToString();
        nameText.text = name;
    }

    public void InitTarget(int level)
    {
        levelText.text = level.ToString();
        indicatorY = 3f + level * 0.3f;
    }
}