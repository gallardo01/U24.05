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

    public void FindPlayer(GameObject character)
    {
        RaycastHit hit;
        Ray ray = new Ray(TF.position, character.transform.position);
        Physics.Raycast(ray,out hit, Mathf.Infinity);
    }

    private Vector3 UpdateOffset(int level)
    {
        offset += new Vector3(0, 3, 2)*level;
        return offset;
    }
}

