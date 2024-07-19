using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform body;
    public float speed = 5.0f;
    public Animator animator;
    private string currentAnim = "idle";
    private FieldOfView fieldOfView;
    
    
    public float meshResolution;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public LineRenderer viewRadiusLine; // Add this line


    // Start is called before the first frame update
    void Start()
    {
        fieldOfView =GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldOfView.visibleTargets.Count > 0)
        {
            ChangeAnim("attack");
        }
    }
    
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
    
}
