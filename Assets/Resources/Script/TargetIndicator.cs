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
    public Camera Camera => CameraFollower.Instance.gameCamera;
    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void LateUpdate()
    {
        viewPoint = Camera.WorldToViewportPoint(character.transform.position + Vector3.forward*3);
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
    }
}
