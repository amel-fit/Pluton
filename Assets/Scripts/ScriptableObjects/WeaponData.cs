using UnityEngine;
using Core;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] public float damage;
        [SerializeField] public float criticalChance;

        public void ApplyDamage(IDamageable damageable, float damage)
        {
            damageable.Health -= damage;
        }
    }
}
