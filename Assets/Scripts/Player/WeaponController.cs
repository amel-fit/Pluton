using System;
using Core;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        private float calculatedDamage = 0;
        
        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponentInParent<IDamageable>();
            
            if (damageable == null) return;
            
            calculatedDamage = weapon.damage * (((Random.value * 100)< weapon.criticalChance) ? 2f : 1f);
            weapon.ApplyDamage(damageable, calculatedDamage);
        }
    }
}
