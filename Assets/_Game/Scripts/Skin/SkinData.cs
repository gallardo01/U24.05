using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinData : ScriptableObject
{

}


[System.Serializable]
public class DataDetail<T> where T : Enum
{
    public T t;
    public int price;
    public Sprite imageSprite;
    public GameObject Prefab;
    public BuffType buffType;
}