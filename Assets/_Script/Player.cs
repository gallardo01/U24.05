using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform center;
    [SerializeField] Transform stackBrick;
    [SerializeField] Transform character;
    [SerializeField] Animator animator;

    public InputAction moveLeft;
    public InputAction moveRight;
    public InputAction moveUp;
    public InputAction moveDown;

    RaycastHit hit;
    bool isRunning = false;

    public GameObject brickPrefabs;

    public enum MoveState
    {
        Up,
        Down,
        Left,
        Right,
        Center
    }
    // Start is called before the first frame update
    void Start()
    {
        moveDown.Enable();
        moveUp.Enable();
        moveRight.Enable();
        moveLeft.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft.triggered)
        {
            if (CheckTheRoad(MoveState.Left) && !isRunning)
            {
                StartCoroutine(AutoMove(MoveState.Left));
            }
        }
        if (moveRight.triggered)
        {
            if (CheckTheRoad(MoveState.Right) && !isRunning)
            {
                StartCoroutine(AutoMove(MoveState.Right));
            }
        }
        if (moveDown.triggered)
        {
            if (CheckTheRoad(MoveState.Down) && !isRunning)
            {
                StartCoroutine(AutoMove(MoveState.Down));
            }
        }
        if (moveUp.triggered)
        {
            if (CheckTheRoad(MoveState.Up) && !isRunning)
            {
                StartCoroutine(AutoMove(MoveState.Up));
            }
        }
        Debug.DrawLine(center.position, center.position + Vector3.down * 2, Color.red);
    }
    private bool CheckTheRoad(MoveState state)
    {
        if (state == MoveState.Up)
        {
            return Physics.Raycast(up.position, Vector3.down, out hit);
        }
        else if (state == MoveState.Down)
        {
            return Physics.Raycast(down.position, Vector3.down, out hit);
        }
        else if (state == MoveState.Right)
        {
            return Physics.Raycast(right.position, Vector3.down, out hit);
        }
        else if (state == MoveState.Left)
        {
            return Physics.Raycast(left.position, Vector3.down, out hit);
        }
        else if (state == MoveState.Center)
        {
            return Physics.Raycast(center.position , Vector3.down, out hit);
        }
        else
        {
            return false;
        }
    }
    private void DestroyBrick(bool check)
    {
        if (check)
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("brick"))
            {
                Destroy(hitObject);
                CreatBrickBeLowPlayer();
            }
        }
    }
    private void ReturnBrickOnLine(bool check)
    {
        if (check)
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("whiteline"))
            {
                CreatBrickOnLine(hitObject.transform);
                DestroyBrickInParentBrick();
            }
        }
    }

    private void CreatBrickOnLine(Transform transform)
    {
        Quaternion rotation = Quaternion.Euler(270f, 0f, -180f);
        Vector3 pos = transform.position;
        GameObject brick = Instantiate(brickPrefabs, new Vector3(pos.x, pos.y + 0.25f, pos.z), rotation);
        brick.GetComponent<Collider>().enabled = false;
        brick.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void DestroyBrickInParentBrick()
    {
        GameObject lastBrick = stackBrick.GetChild(stackBrick.childCount - 1).gameObject;
        Debug.Log("destroy");
        Destroy(lastBrick);
    }

    private void CreatBrickBeLowPlayer()
    {
        Quaternion rotation = Quaternion.Euler(270f, 0f, -180f);
        Vector3 pos = center.position;
        GameObject brick = Instantiate(brickPrefabs, new Vector3(pos.x, pos.y+stackBrick.childCount*0.25f, pos.z), rotation);
        brick.transform.SetParent(stackBrick, true);
        brick.GetComponent<Rigidbody>().isKinematic = true;
        brick.GetComponent<Collider>().enabled = false;
    }
    private void MovePlayer(bool check)
    {
        if (check)
        {
            GameObject hitObject = hit.collider.gameObject;
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(hitObject.transform.position.x, currentPos.y, hitObject.transform.position.z);     
            character.position = new Vector3(hitObject.transform.position.x, currentPos.y + 0.75f + stackBrick.childCount*0.25f, hitObject.transform.position.z);
        }
    }
    IEnumerator AutoMove(MoveState state)
    {
        if (CheckTheRoad(state))
        {
            isRunning = true;
            MovePlayer(CheckTheRoad(state));
            DestroyBrick(CheckTheRoad(MoveState.Center));
            ReturnBrickOnLine(CheckTheRoad(MoveState.Center));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(AutoMove(state));
        }
        else
        {
            MoveNavigation(CheckTheRoad(MoveState.Center), state);
            isRunning = false;
        }
    }
    //Ve nha lam tiep
    private void GoToPushBrick(MoveState currentstate)
    {
        List<MoveState> vertical = new List<MoveState> { MoveState.Up, MoveState.Down};
        List<MoveState> horizontal = new List<MoveState> { MoveState.Left, MoveState.Right};
        if (horizontal.Contains(currentstate))
        {
            for (int i = 0; i < vertical.Count; i++)
            {
                if (CheckTheRoad(vertical[i]))
                {
                    StartCoroutine(AutoMove(vertical[i]));
                }
            }
        }
        if (vertical.Contains(currentstate))
        {
            for (int i = 0; i < horizontal.Count; i++)
            {
                if (CheckTheRoad(horizontal[i]))
                {
                    StartCoroutine(AutoMove(horizontal[i]));
                }
            }
        }
    }
    //check huong di tiep theo
    private void MoveNavigation(bool check, MoveState state)
    {
        if (check)
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("pushbrick"))
            {
                GoToPushBrick(state);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("final"))
        {

            stackBrick.gameObject.SetActive(false);
            transform.SetParent(collision.gameObject.transform);
            transform.localPosition = new Vector3(0f, -13f, -5f);
            animator.SetInteger("renwu", 2);
        }
    }
}

