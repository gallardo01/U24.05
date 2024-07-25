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

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        TF.position = TFPlayer.position + offset;
        for (int i = 0; i < GameController.instance.countPlayers.Count; i++)
        {
            GameObject player = GameController.instance.countPlayers[i];
            Debug.DrawLine(TF.position, player.transform.position, Color.blue, 5f);
        }
        
    }

    public void FindPlayer(GameObject character)
    {
        RaycastHit hit;
        Ray ray = new Ray(TF.position, character.transform.position);
        Physics.Raycast(ray,out hit, Mathf.Infinity);
    }
}

