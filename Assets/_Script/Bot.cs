using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    IState<Bot> currentState;
    public NavMeshAgent agent;
    public Transform target;
    public float time;
    public float randomRadius = 30f;
    float detectObtaclesRadius = 10f;
    float cooldownMove = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        currentState = new IdleState();   
        time = cooldownMove;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= cooldownMove)
        {
            currentState.OnExecute(this);
            time = 0;
        }

        if (time > 0.9f)
        {
            isAttack = false;
        }

    }

    public void ChangeState(IState<Bot> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public bool IsObstacleDetected()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,detectObtaclesRadius,-1);
        return hitColliders.Length > 0;
    }

    public void ChangeDirection(Vector3 origin, float dist, int layermask)
    {
        Vector3 finalPosition = RandomNavSphere(origin, dist, layermask);
        agent.SetDestination(finalPosition);
    }
}

