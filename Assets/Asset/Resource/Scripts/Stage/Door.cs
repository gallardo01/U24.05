using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform pivotPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            if (character.characterBrick.BrickNumbers >= 0)
            {
                StartCoroutine(nameof(DoorWork));
            }
        }
    }

    IEnumerator DoorWork()
    {
        float time = 0;

        while (time < 2)
        {
            time += Time.deltaTime;
            transform.RotateAround(pivotPoint.position, Vector3.up, 90/2 * Time.deltaTime);
            yield return null;
        }
    }
}
