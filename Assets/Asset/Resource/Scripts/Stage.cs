using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private StageController stageController;
    public StageController StageController => this.stageController;
}
