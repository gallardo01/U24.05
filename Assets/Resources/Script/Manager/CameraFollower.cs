using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour, IGameStateListener
{
    private Transform playerTF;
    private Vector3 offset;
    private Vector3 camRotation;
    private float fieldOfView;

    private void Start()
    {
        FindPlayer();
    }

    private void LateUpdate()
    {
        if (playerTF != null)
        {
            transform.position = Vector3.Lerp(transform.position, playerTF.position + offset, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Euler(camRotation);
        }
        else FindPlayer();
    }
    private void FindPlayer()
    {
        playerTF = PlayersManager.Instance.player.transform;
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                offset = new Vector3(0f, 1f, 3f);
                camRotation = new Vector3(0f, 180f, 0f);
                this.GetComponent<Camera>().fieldOfView = 80;
                break;

            case GameState.GAME:
                offset = new Vector3(0f, 10f, -7.5f);
                camRotation = new Vector3(50f, 0f, 0f);
                this.GetComponent<Camera>().fieldOfView = 60;
                break;

        }
    }
}
