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
    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    public void OnInit(Character character)
    {
        charName.text = Constant.Char_Names_List[Random.Range(0, Constant.Char_Names_List.Count)] + Random.Range(0, 1000);
        levelText.text = "0";
        levelImage.color = Random.ColorHSV();
        this.character = character;
    }

    public void UpdateLevel(int level)
    {
        levelText.text = level.ToString();
    }

    private void LateUpdate()
    {
        viewPoint = Camera.main.WorldToViewportPoint(character.transform.position + Vector3.up * 5);
        GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToScreenPoint(viewPoint) - screenHalf;
    }

}
