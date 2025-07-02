using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyCombat", menuName = "Scriptable Objects/Enemy/EnemyCombat")]
    public class EnemyCombat : ScriptableObject
    {
        [SerializeField] private float health;
        [SerializeField] public float damage = 20;
        
        public float GetHealth() => health;
    }
}
