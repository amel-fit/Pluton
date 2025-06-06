using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyCombat", menuName = "Scriptable Objects/Enemy/EnemyCombat")]
    public class EnemyCombat : ScriptableObject
    {
        [SerializeField] private CharacterCharacteristicsData characteristicsData;
        public float GetHealth() => characteristicsData.characteristics.StartingHealth;
    }
}
