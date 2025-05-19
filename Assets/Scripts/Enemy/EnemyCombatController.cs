using System;
using ScriptableObjects;
using UnityEngine;

namespace Enemy
{
    public class EnemyCombatController : MonoBehaviour
    {
        [SerializeField] private EnemyCombat combat;
        private float health;

        private void Start()
        {
            health = combat.GetHealth();
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            Debug.Log($"took {damage} damage :: {health}");
        }
    }
}
