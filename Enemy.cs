using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Movement))]
public class Enemy : MonoBehaviour
{
    public float lookRadius = 10f;
    public float rotateSpeed = 5f;
    public float waitTime = 3f;

    public bool isSeen = false;
    public bool nextWayPoint = false;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    Movement movement;

    NavMeshAgent enemy;

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GameObject.Find("Character").GetComponent<Movement>();
        enemy = GetComponent<NavMeshAgent>();
        enemy.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        if (!isSeen)
        {
            anim.SetBool("isseen", false);
            enemy.speed = 2f;
            if (enemy.remainingDistance < enemy.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                enemy.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }

        if (isSeen)
        {
            anim.SetBool("isseen", true);
            enemy.speed = 15f;
            float distance = Vector3.Distance(transform.position, movement.transform.position);
            if (distance <= lookRadius)
            {
                anim.SetBool("ishitting", false);
                anim.SetBool("isrunning", true);
                enemy.SetDestination(movement.transform.position);
                if (distance <= enemy.stoppingDistance)
                {
                    FaceTarget();
                }
            }
            else
            {
                anim.SetBool("ishitting", false);
                anim.SetBool("isrunning", false);
            }
            if (distance <= enemy.stoppingDistance)
            {
                anim.SetBool("isrunning", false);
                if (movement.canMove)
                {
                    anim.SetBool("ishitting", true);
                }
            }
        }
    }
    public void FaceTarget()
    {
        Vector3 direction = (movement.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isSeen = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
