using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItem : MonoBehaviour
{
    [SerializeField] GameObject imageChoosing, Lock, Equipped;
    [SerializeField] Button button;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            imageChoosing.SetActive(true);
        });
    }
}
