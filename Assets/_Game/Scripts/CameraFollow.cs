using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform TF;
    [SerializeField] Transform targetTF;

    [SerializeField] Transform offsetGameplay;
    [SerializeField] Transform offsetMainmenu;

    private Vector3 OffsetGameplay => offsetGameplay.localPosition * LevelManager.Ins.player.LevelScale;

    private bool isRotateFinish = true;
    private Quaternion targetRotation;
    private Vector3 targetPosition;

    private void Awake()
    {
        this.RegisterListener(EventID.OnGameStateChanged, (param) =>
        {
            GameState gameState = (GameState)param;

            if (gameState == GameState.Mainmenu)
            {
                SetTargetRotate(offsetMainmenu.localRotation);
            }
            else if (gameState == GameState.Gameplay)
            {
                SetTargetRotate(offsetGameplay.localRotation);
            }

            
        });
    }

    private void LateUpdate()
    {
        if (GameManager.Ins.IsState(GameState.Mainmenu))
        {
            targetPosition = targetTF.position + offsetMainmenu.localPosition;
        }
        else if (GameManager.Ins.IsState(GameState.Gameplay))
        {
            targetPosition = targetTF.position + OffsetGameplay;
        }

        TF.position = Vector3.Lerp(TF.position, targetPosition, Time.deltaTime * 5f);
        //TF.position = targetPosition;

        if (!isRotateFinish)
        {
            TF.rotation = Quaternion.Slerp(TF.rotation, targetRotation, Time.deltaTime * 5f);
            if (TF.rotation == targetRotation)
            {
                isRotateFinish = true;
            }
        }
    }

    private void SetTargetRotate(Quaternion targetRotate)
    {
        this.targetRotation = targetRotate;
        isRotateFinish = false;
    }
}
