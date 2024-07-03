using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bot : Character
{
    [SerializeField] Transform body;
    GameObject moveBrick;

    private void Update()
    {
        //moveBrick = AutoFindTheNearestBrickCanPick();
        //MoveToTheBrick(moveBrick);
    }

    private GameObject AutoFindTheNearestBrickCanPick()
    {
        List<GameObject> bricks = StageControler.Instance.bricksListStage1;
        GameObject nearestBrick = null;
        float minDistance = Mathf.Infinity;
        while (true)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].GetComponent<Brick>().brickColor == this.colorIndex)
                {
                    float distance = Vector3.Distance(transform.position, bricks[i].transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestBrick = bricks[i];
                    }
                }
            }
            if (nearestBrick != null)
            {
                return nearestBrick;
            } else
            {
                return null;
            }           
        }
    }
    private void MoveToTheBrick(GameObject brick)
    {
        if (brick != null)
        {
            Vector3 direction = brick.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(direction);
            body.rotation = newRotation;
            Vector3 nextPoint = transform.position + direction.normalized * speed * Time.deltaTime;
            ChangeAnim("run");
            transform.position = nextPoint;
        }
        else
        {
            ChangeAnim("idle");
        }
    }
}
