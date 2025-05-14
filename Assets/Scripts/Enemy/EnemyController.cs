using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase
    }

    [Header("Enemy Settings")]
    public float moveSpeed = 3f;
    public float detectionRadius = 5f;
    public float patrolWaitTime = 2f;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    public bool randomPatrol = true;
    
    private Transform playerTransform;
    private NavMeshAgent agent;
    private Animator animator;
    private EnemyState currentState;
    
    private int currentPatrolIndex = 0;
    private float patrolTimer = 0f;
    private bool isWaitingAtPatrolPoint = false;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (agent != null)
        {
            agent.speed = moveSpeed;
            agent.stoppingDistance = 2f;
            agent.updateRotation = false;
            agent.angularSpeed = 999;
        }
        
        currentState = patrolPoints.Length > 0 ? EnemyState.Patrol : EnemyState.Idle;
    }
    
    Vector3 GetMovementDirection()
    {
        if (agent != null && agent.velocity.magnitude > 0.1f)
        {
            Vector3 velocity = agent.velocity;
            velocity.y = 0;
            return velocity.normalized;
        }
        return Vector3.zero;
    }
    public void Update()
    {
        if (playerTransform == null) return;

        Vector3 playerPos = playerTransform.position;
        Vector3 myPos = transform.position;

        float distanceToPlayer = Vector3.Distance(
            new Vector3(myPos.x, 0, myPos.z), 
            new Vector3(playerPos.x, 0, playerPos.z)
        );
        
        if (distanceToPlayer <= detectionRadius && HasLineOfSight())
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = patrolPoints.Length > 0 ? EnemyState.Patrol : EnemyState.Idle;
        }

        switch (currentState)
        {
            case EnemyState.Idle:
                IdleBehavior();
                break;
                
            case EnemyState.Patrol:
                PatrolBehavior();
                break;
                
            case EnemyState.Chase:
                ChaseBehavior();
                break;
        }
        
        if (currentState == EnemyState.Chase)
        {
            if (playerTransform != null)
            {
                RotateTowards(playerTransform.position);
            }
        }
        else if (currentState == EnemyState.Patrol && !isWaitingAtPatrolPoint)
        {
            Vector3 movementDir = GetMovementDirection();
            
            if (movementDir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movementDir);
            }
            else if (patrolPoints.Length > 0)
            {
                RotateTowards(patrolPoints[currentPatrolIndex].position);
            }
        }
    }
    
    void IdleBehavior()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
        animator.SetFloat("Speed", 0f);
    }
    
    void ChaseBehavior()
    {
        if (agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            direction.y = 0;
            
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        animator.SetFloat("Speed", 1f);
    }
    
    void PatrolBehavior()
    {
        animator.SetFloat("Speed", 1f);
        
        if (patrolPoints.Length == 0)
        {
            currentState = EnemyState.Idle;
            return;
        }
        
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        
        if (isWaitingAtPatrolPoint)
        {

            if (agent != null)
            {
                agent.isStopped = true;
                animator.SetFloat("Speed", 0f);
            }
            
            patrolTimer += Time.deltaTime;
            
            if (patrolTimer >= patrolWaitTime)
            {
                isWaitingAtPatrolPoint = false;
                patrolTimer = 0f;
                
                int previousIndex = currentPatrolIndex;

                if (randomPatrol)
                {
                    if (patrolPoints.Length > 1)
                    {
                        do
                        {
                            currentPatrolIndex = Random.Range(0, patrolPoints.Length);
                        } 
                        while (currentPatrolIndex == previousIndex);
                    }
                }
                else
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                }

            }
        }
        else
        {
            Vector3 targetPos = targetPoint.position;
            Vector3 myPos = transform.position;
            float distanceToPoint = Vector3.Distance(
                new Vector3(myPos.x, 0, myPos.z),
                new Vector3(targetPos.x, 0, targetPos.z)
            );
            
            if (distanceToPoint < 0.5f) 
            {
                isWaitingAtPatrolPoint = true;
                if (agent != null)
                {
                    agent.isStopped = true;
                    animator.SetFloat("Speed", 0f);
                }
            }
            else
            {
                if (agent != null)
                {
                    agent.isStopped = false;
                    animator.SetFloat("Speed", 1f);
                    agent.SetDestination(targetPoint.position);
                }
                else
                {
                    Vector3 direction = (targetPoint.position - transform.position).normalized;
                    direction.y = 0;
                    
                    transform.position += direction * moveSpeed * Time.deltaTime;
                }
            }
        }
    }
    
    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        
        if (direction != Vector3.zero)
        {
            transform.forward = direction.normalized;
        }
    }
    
    bool HasLineOfSight()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f; 
        Vector3 target = playerTransform.position + Vector3.up * 0.5f;
        Vector3 direction = target - origin;
        float distance = 20f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            return hit.collider.CompareTag("Player"); 
        }

        return false;
    }
}