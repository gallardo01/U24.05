using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform target;
    [SerializeField] Transform tf;
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textLevel;

    void LateUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        tf.position = screenPos + offset;
    }

    public void UpdateTextLevel(int level)
    {
        textLevel.text = level.ToString();
    }

    public void UpdateTextName(string name)
    {
        textName.text = name;
    }

    public string GetTextName()
    {
        return textName.text;
    }

    public void SetActiveCharacterInfo(bool isActive)
    {
        textName.gameObject.SetActive(isActive);
        textLevel.gameObject.SetActive(isActive);
    }
}
