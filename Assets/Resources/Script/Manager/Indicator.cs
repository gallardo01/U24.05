using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image levelImage;
    private Character character;
    private int level;
    private RectTransform rectTransform;
    private Camera mainCamera;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    private void Awake()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnInit(Character character)
    {
        charName.text = Constant.Char_Names_List[Random.Range(0, Constant.Char_Names_List.Count)] + Random.Range(0, 1000);
        levelText.text = "0";
        levelImage.color = Random.ColorHSV();
        this.character = character;
    }

    public void UpdateLevel()
    {
        level++;
        levelText.text = level.ToString();
        rectTransform.anchoredPosition += Vector2.up * 2;
    }

    private void LateUpdate()
    {
       rectTransform.anchoredPosition = mainCamera.WorldToScreenPoint(character.transform.position + Vector3.up * 3) - screenHalf;
    }
}
