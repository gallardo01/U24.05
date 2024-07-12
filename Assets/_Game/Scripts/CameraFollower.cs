using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Transform TF;
    [SerializeField] Transform targetTF;

    [SerializeField] Vector3 offset;

    private void Start()
    {
        this.RegisterListener(EventID.OnInitPlayer, (param) => SetTargetTF((Transform) param));
        this.RegisterListener(EventID.OnGameFinish, (param) => SetTargetTF((Transform) param));
    }

    private void LateUpdate()
    {
        if (targetTF == null)
        {
            return;
        }

        TF.position = Vector3.Lerp(TF.position, targetTF.position + offset, Time.deltaTime * 5f);
    }

    private void SetTargetTF(Transform param)
    {
        targetTF = param;
    }
}
