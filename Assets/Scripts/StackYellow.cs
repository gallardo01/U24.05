using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackYellow : MonoBehaviour
{
    [SerializeField] Transform Player;
    private float jumpHight = 0.5f;

    private float stackCount = 0;
    private float objectHight = 0.3f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stackable")) 
        {
            Debug.Log("Đối tượng có thể xếp chồng được phát hiện.");
            //Jump the player up
            Vector3 newPos = Player.position;
            newPos.y += jumpHight;
            Player.position = newPos;

            //Put the object below the player
            Transform t = other.transform;
            t.tag = "Untagged";
            t.SetParent(this.transform);
            t.localPosition = new Vector3(0, stackCount * objectHight, 0);

            stackCount++;
            Debug.Log("Đối tượng đã xếp chồng. Số lượng xếp chồng hiện tại: " + stackCount);
        }
    }
}
