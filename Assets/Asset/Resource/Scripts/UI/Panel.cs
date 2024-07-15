using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private string panelName;

    void Awake()
    {
        panelName = this.gameObject.name;
        MenuController.Instance.RegisterPanel(panelName, this.gameObject);
    }
}
