using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyCombatController : MonoBehaviour, IEntity, IDamageable
    {
        [SerializeField] private float knockbackForce = 15f;
        [SerializeField] private List<GameObject> Coins;
        
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
                DropCoins();
            }
            else
            {
                //animator.SetTrigger("Hit");
                Knockback();
            }
        }

        private void DropCoins()
        {
            int numOfCoins = Random.Range(1, 10);
            if (numOfCoins >= 6)
            {
                Instantiate(Coins[0], new Vector3(transform.position.x, 0.1f,transform.position.z) , transform.rotation);
            }
            else if(numOfCoins >= 2)
            {
                Instantiate(Coins[1], new Vector3(transform.position.x, 0.1f,transform.position.z) , transform.rotation);
            }
            else
            {
                Instantiate(Coins[2], new Vector3(transform.position.x, 0.1f,transform.position.z) , transform.rotation);
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
