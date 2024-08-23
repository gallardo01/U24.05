using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Bot : Character
{
    [SerializeField] public GameObject inAreaAtack;

    IState<Bot> currentState;
    public NavMeshAgent agent;
    public float time;
    public float randomRadius = 30f;
    float cooldownMove = 1.5f;
    public bool isHiding = false;

    private void Start()
    {
        SetNewPlayer();
        cooldownTimeAttack = Random.Range(1f, 2.5f);
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= cooldownMove && isDead == false)
        {
            currentState.OnExecute(this);
            time = 0;
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        time = cooldownMove;
        inAreaAtack.SetActive(false);
        currentState = new IdleState();
        gameObject.tag = "bot";
    }
    public override void OnDeath()
    {
        base.OnDeath();
        inAreaAtack.SetActive(false);
        Destroy(gameObject,3f);
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

    public List<GameObject> FindTarget()
    {
        List<GameObject> listTarget = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, -1);
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (collider.CompareTag("player") || collider.CompareTag("bot"))
                {
                    listTarget.Add(collider.gameObject);
                }
            }
        }
        return listTarget;
    }

    public void ChooseTargetPriority(List<GameObject> listTarget)
    {
        float minHealth = Mathf.Infinity;
        if (listTarget.Count > 0)
        {
            for (int i = 0; i < listTarget.Count; i++)
            {
                if (listTarget[i].GetComponent<Character>().health < minHealth)
                {
                    minHealth = listTarget[i].GetComponent<Character>().health;
                    target = listTarget[i];
                }
            }
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    public override void LevelUpData()
    {
        base.LevelUpData();
        IncreaseStatOfBot(level);
    }

    private void IncreaseStatOfBot(int level)
    {
        if (level == 0)
        {
            BASE_ATTACK = 35;
        }
        attack = BASE_ATTACK + 3 * level;
        defend = BASE_DEFEND + 3 * level;
    }
}


