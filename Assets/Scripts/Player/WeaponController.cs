using System;
using Enemy;
using ScriptableObjects;
using UnityEngine;
using Random = System.Random;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        private float damage = 0;
        private Random r = new Random();
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                damage = weapon.damage * (((r.Next() % 100)< weapon.criticalChance) ? 2f : 1f);
                //Debug.Log("Enemy hit!");
                other.GetComponentInParent<EnemyCombatController>().TakeDamage(damage);
            }
        }
        
    }
}
