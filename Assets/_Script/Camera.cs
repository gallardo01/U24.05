using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Camera : MonoBehaviour
{
    [SerializeField] public Transform TF;
    [SerializeField] Transform TFPlayer;

    public Vector3 offset;
    public static Camera instance;
    public int levelPlayer;

    private void Awake()
    {
        instance = this;
        levelPlayer = TFPlayer.GetComponent<Character>().level;

    }
    // Update is called once per frame
    void Update()
    {
        TF.position = TFPlayer.position + offset;
    }

    private Vector3 UpdateOffset(int level)
    {
        offset += new Vector3(0, 3, 2)*level;
        return offset;
    }
}

