using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<GameObject> weapons;
    public List<GameObject> heads;
    public List<GameObject> shield;
    public List<Material> pants;
    private void Awake()
    {
        instance = this;
    }
}
