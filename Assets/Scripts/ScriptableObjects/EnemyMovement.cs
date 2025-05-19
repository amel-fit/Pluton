using UnityEngine;
using UnityEngine.AI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyMovement", menuName = "Scriptable Objects/Enemy/EnemyMovement")]
    public class EnemyMovement : ScriptableObject
    {
        [Header("Enemy Settings")]
        [SerializeField] public float moveSpeed = 3f;
        [SerializeField] public float detectionRadius = 5f;
        [SerializeField] public float patrolWaitTime = 2f;
        
        
    
    
        [Header("Patrol Settings")]
        public Transform[] patrolPoints;
        public bool randomPatrol = true;
    
        private Transform playerTransform;
        private NavMeshAgent agent;
        private Animator animator;
    
        private float patrolTimer = 0f;
        
    }
}
