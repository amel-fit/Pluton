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
        [SerializeField] private float knockbackForce = 15f;
        
        private Animator animator;
        private Rigidbody rb;
        private Transform playerSource;

        [SerializeField] private GameObject weapon;
        private Vector3 knockbackDirection;
        
        [field: SerializeField]
        public CharacterCharacteristics Characteristics { get; set; }
        
        [SerializeField]
        private CharacterCharacteristicsData CharacteristicsData;
        
        [field: SerializeField]
        public float Health { get; set; }

        public void TakeDamage(float damage, IDamageable source)
        {
            Health -= damage;
            Debug.Log($"took {damage} damage :: {Health}");
            if (Health <= 0)
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
            knockbackDirection = transform.position - playerSource.position;
            knockbackDirection.y = 0f;
            knockbackDirection.Normalize();

            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
        
        private void WeaponCollisionOn() => weapon.GetComponent<Collider>().enabled = true;
        private void WeaponCollisionOff() => weapon.GetComponent<Collider>().enabled = false;
    }
}
