using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyCombatController : MonoBehaviour
    {
        [SerializeField] private EnemyCombat combat;
        [SerializeField] float knockbackForce = 15f;
        private float health;
        private Animator animator;
        private Rigidbody rb;
        private Transform playerSource;
        

        private void Start()
        {
            health = combat.GetHealth();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            playerSource = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Knockback()
        {
            Vector3 knockbackDir = (transform.position - playerSource.position).normalized;
            rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
        }
        
        
        public void TakeDamage(float damage)
        {
            health -= damage;
            Debug.Log($"took {damage} damage :: {health}");
            if (health <= 0)
            {
                GetComponent<EnemyAI>().enabled = false;
                animator.SetTrigger("Die");
                Destroy(gameObject,2.4f);
            }
            else
            {
                //animator.SetTrigger("Hit");
                Knockback();
            }
        }
        
    }
}
