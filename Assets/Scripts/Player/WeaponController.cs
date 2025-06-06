using System;
using Core;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [FormerlySerializedAs("weapon")] [SerializeField] private WeaponData weaponData;
        private float calculatedDamage = 0;
        
        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponentInParent<IDamageable>();
            
            if (damageable == null) return;
            
            calculatedDamage = weaponData.damage * (((Random.value * 100)< weaponData.criticalChance) ? 2f : 1f);
            weaponData.ApplyDamage(damageable, calculatedDamage);
        }
    }
}
