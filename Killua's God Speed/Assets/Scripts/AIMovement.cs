using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class AIMovement : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector]
    public Transform player;

    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public Animator animator;

    public Transform goal;

    public VisualEffect speedUpBurst;

    public float runningRadius = 40f;
    public float runSpeed = 20f;

    [Range(0, 100)]
    public float turnSpeed = 50f;

    [HideInInspector]
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.instance.player.transform;
        rb = GetComponent<Rigidbody>();

        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }

        speedUpBurst = GetComponentInChildren<VisualEffect>();
    }

    public virtual void Running()
    {
        distance = Vector3.Distance(transform.position, player.position);

        agent.SetDestination(goal.position);
        agent.speed = runSpeed;
    }

    public virtual void Rotation()
    {
        Vector3 lookDirection = (goal.position - transform.position).normalized;

        if (distance > 0)
        {
            Quaternion turn = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, turn, Time.deltaTime * turnSpeed);
        }

    }

    public void ObjectiveFail()
    {
        //Put what happens if the enemy fail's its objective
        GameManager.instance.PlayerWins();
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, runningRadius);
    }

    // Update is called once per frame
    void Update()
    {
        Running();
        Rotation();
    }
}
