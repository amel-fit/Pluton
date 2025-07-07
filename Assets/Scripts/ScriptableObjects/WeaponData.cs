using UnityEngine;
using Core;
using UnityEngine.UIElements;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] public float damage;
        [SerializeField] public float criticalChance;

        public void ApplyDamage(IDamageable damageable, float damage, IDamageable source)
        {
            //i want to handle damage taking elsewhere
            //damageable.Health -= damage;
            damageable.TakeDamage(damage, source);
        }
    }
}
