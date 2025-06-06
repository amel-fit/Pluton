using System;
using System.Collections;
using Core;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyCombatController : MonoBehaviour, IEntity, IDamageable
    {
        [SerializeField] float knockbackForce = 15f;
        
        private Animator animator;
        private Rigidbody rb;
        private Transform playerSource;
        
        [field: SerializeField]
        public CharacterCharacteristics Characteristics { get; set; }
        
        [SerializeField]
        private CharacterCharacteristicsData CharacteristicsData;
        
        [field: SerializeField]
        public float Health { get; set; }

        public void TakeDamage()
        {
            throw new NotImplementedException();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            playerSource = GameObject.FindGameObjectWithTag("Player").transform;
            
            Characteristics = new CharacterCharacteristics()
            {
                Dexterity = CharacteristicsData.characteristics.Dexterity,
                Strength = CharacteristicsData.characteristics.Strength,
                StartingHealth = CharacteristicsData.characteristics.StartingHealth
            };
            
            Health = Characteristics.StartingHealth;
        }

        private void Knockback()
        {
            Vector3 knockbackDir = (transform.position - playerSource.position).normalized;
            rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
        }
        
        // public void TakeDamage(float damage)
        // {
        //     Health -= damage;
        //     Debug.Log($"took {damage} damage :: {health}");
        //     if (health <= 0)
        //     {
        //         GetComponent<EnemyAI>().enabled = false;
        //         animator.SetTrigger("Die");
        //         Destroy(gameObject,2.4f);
        //     }
        //     else
        //     {
        //         //animator.SetTrigger("Hit");
        //         Knockback();
        //     }
        // }
    }
}
