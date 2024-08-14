using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform target;
    [SerializeField] Transform tf;
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textLevel;
    [SerializeField] Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    //Vector3 viewPoint;
    //Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;
    //private float offsetY = 3f;

    void LateUpdate()
    {
        //viewPoint = Camera.main.WorldToViewportPoint(target.position + Vector3.up * offsetY);
        //tf.position = Camera.main.ViewportToScreenPoint(viewPoint);

        Vector3 screenPos = _camera.WorldToScreenPoint(target.position);
        tf.position = screenPos + offset;

        //tf.rotation = Quaternion.LookRotation(tf.position - _camera.transform.position);
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
