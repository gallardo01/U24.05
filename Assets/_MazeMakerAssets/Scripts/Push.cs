using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{

    [SerializeField] MoveState moveState;

    public MoveState GetMoveState() { return moveState; }
}
