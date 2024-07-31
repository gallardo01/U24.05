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

    void FixedUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        tf.position = screenPos + offset;
    }

    public void UpdateTextLevel(int level)
    {
        textLevel.text = level.ToString();
    }

    public void SetActiveCharacterInfo(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
