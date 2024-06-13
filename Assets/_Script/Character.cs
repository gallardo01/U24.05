using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Character : MonoBehaviour
{
    public GameObject brickPrefabs;
    float countBrick;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "brick")
        {
            Vector3 currentPosBrick = other.gameObject.transform.position;
            Debug.Log(currentPosBrick + "vi tri cu");
            Destroy(other.gameObject);
            countBrick++;
            GroupBrickBeLowCharacter(countBrick, currentPosBrick);
        }
    }
    private void GroupBrickBeLowCharacter(float count, Vector3 pos)
    {
        Vector3 offset = new Vector3(0, count*0.2f, 0);
        brickPrefabs = Instantiate(brickPrefabs, pos + offset, brickPrefabs.transform.rotation);
        Debug.Log(brickPrefabs.transform.position + "vi tri moi");
        //transform.position += offset;

    }
}
