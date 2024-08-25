using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour, IGameStateListener
{
    private Transform player;
    private Vector3 offset;
    private Vector3 camRotation;
    private float fieldOfView;
    [SerializeField] Camera cam;

    [Header("MENU State")]
    [SerializeField] Vector3 menuOffset;
    [SerializeField] Vector3 menuRotate;
    [SerializeField] float menuFOV;

    [Header("SHOP State")]
    [SerializeField] Vector3 shopOffset;
    [SerializeField] Vector3 shopRotate;
    [SerializeField] float shopFOV;

    [Header("GAME State")]
    [SerializeField] Vector3 gameOffset;
    [SerializeField] Vector3 gameRotate;
    [SerializeField] float gameFOV;

    [Header("GAMEOVER State")]
    [SerializeField] Vector3 gameoverOffset;
    [SerializeField] Vector3 gameoverRotate;
    [SerializeField] float gameoverFOV;

    private void OnInit()
    {
        player = CharactersManager.Instance.player.transform;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Euler(camRotation);
        }
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                if (player == null) { Invoke(nameof(OnInit), 0.01f); }
                offset = menuOffset;
                camRotation = menuRotate;
                cam.fieldOfView = menuFOV;
                break;

            case GameState.GAME:
                offset = gameOffset;
                camRotation = gameRotate;
                cam.fieldOfView = gameFOV;
                break;

            case GameState.SHOP:
                offset = shopOffset;
                camRotation = shopRotate;
                cam.fieldOfView = shopFOV;
                break;

            case GameState.GAMEOVER:
                offset = gameoverOffset;
                camRotation = gameoverRotate;
                cam.fieldOfView = gameoverFOV;
                break;
        }
    }
}
