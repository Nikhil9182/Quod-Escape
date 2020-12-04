using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private float lookRadius = 10f;
    [SerializeField]
    private float rotateSpeed = 5f;
    [SerializeField]
    private int enemyID;
    [SerializeField]
    float sightRadius = 14f;

    public bool sighted = false;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //myEnemy = GetComponent<GameObject>();
        GameController.instance.enemy[enemyID].SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        if (!GameController.instance.isSeen)
        {
            anim.SetBool("isseen", false);
            GameController.instance.enemy[enemyID].speed = GameController.instance.initialSpeed;
            if (GameController.instance.enemy[enemyID].remainingDistance < GameController.instance.enemy[enemyID].stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                GameController.instance.enemy[enemyID].SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            Vector3 direction = GameController.instance.player.transform.position - GameController.instance.enemy[enemyID].transform.position;
            float angle = Vector3.Angle(direction, GameController.instance.enemy[enemyID].transform.forward);
            if (angle < GameController.instance.fovAngle * 0.5)
            {
                RaycastHit hit;
                if(Physics.Raycast(GameController.instance.enemy[enemyID].transform.position,direction.normalized,out hit,sightRadius))
                {
                    if(hit.collider.gameObject.CompareTag("Player"))
                    {
                        GameController.instance.isSeen = true;
                    }
                }
            }
        }

        if (GameController.instance.isSeen)
        {
            if(GameController.instance.pointLight[enemyID].range == 0f)
            {
                GameController.instance.pointLight[enemyID].range = 4f;
            }
            anim.SetBool("isseen", true);
            GameController.instance.enemy[enemyID].speed = 8f;
            float distance = Vector3.Distance(transform.position, GameController.instance.player.transform.position);
            if (distance <= lookRadius)
            {
                anim.SetBool("ishitting", false);
                anim.SetBool("isrunning", true);
                GameController.instance.enemy[enemyID].SetDestination(GameController.instance.player.transform.position);
                if (distance <= GameController.instance.enemy[enemyID].stoppingDistance)
                {
                    FaceTarget();
                }
            }
            else
            {
                anim.SetBool("ishitting", false);
                anim.SetBool("isrunning", false);
                if(GameController.instance.enemy[enemyID].acceleration != 0)
                {
                    anim.SetBool("isrunning", true);
                }
            }
            if (distance <= GameController.instance.enemy[enemyID].stoppingDistance)
            {
                anim.SetBool("isrunning", false);
                if (GameController.instance.canMove)
                {
                    anim.SetBool("ishitting", true);
                }
            }
        }
    }
    public void FaceTarget()
    {
        Vector3 direction = (GameController.instance.player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
