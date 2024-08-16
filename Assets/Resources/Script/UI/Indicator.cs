using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image levelImage;
    private Character character;
    private int level;
    private RectTransform indicator;
    private RectTransform canvasRectTransform;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    public void OnInit(Character character)
    {
        charName.text = Constant.Char_Names_List[Random.Range(0, Constant.Char_Names_List.Count)] + Random.Range(0, 1000);
        levelText.text = "0";
        levelImage.color = Random.ColorHSV();
        this.character = character;

        canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        indicator = GetComponent<RectTransform>();
    }

    public void UpdateLevel()
    {
        level++;
        levelText.text = level.ToString();
        indicator.anchoredPosition += Vector2.up * 2;
    }

    private void LateUpdate()
    {
        CaculatePos();
    }

    private void CaculatePos()
    {
        float offset = 10f;   // Offset from the screen border
        Vector3 viewPos = Camera.main.WorldToViewportPoint(character.IndicatorPoint.position);

        // Determine if the character is outside the viewport
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            viewPos.x = Mathf.Clamp(viewPos.x, 0.01f, 0.99f);
            viewPos.y = Mathf.Clamp(viewPos.y, 0.01f, 0.99f);

            Vector2 screenPos = Camera.main.ViewportToScreenPoint(viewPos);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPos, null, out Vector2 canvasPos);

            indicator.anchoredPosition = canvasPos;
        }
        else
        {
            Vector2 screenPos = Camera.main.ViewportToScreenPoint(viewPos);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPos, null, out Vector2 canvasPos);

            indicator.anchoredPosition = canvasPos + Vector2.one * offset;
        }
    }

}
