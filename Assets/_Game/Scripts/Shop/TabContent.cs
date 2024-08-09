using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabContent<T> : MonoBehaviour where T : Enum
{
    [SerializeField] protected T t;
    [SerializeField] protected Button btnTab;
    [SerializeField] protected ShopItem<T> shopItemPrefab;
    [SerializeField] protected Transform contentParent;
}
