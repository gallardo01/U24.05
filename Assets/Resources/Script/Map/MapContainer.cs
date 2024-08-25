using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapContainer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image icon;
    [field: SerializeField] public Button Button {  get; private set; }
    public MapData Data { get; private set; }

    public void Configue(MapData data)
    {
        this.nameText.text = data.name;
        this.icon.sprite = data.icon;
        this.Data = data;
    }
    
}
