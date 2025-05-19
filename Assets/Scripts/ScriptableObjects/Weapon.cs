using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] public float damage;
        [SerializeField] public float criticalChance;
    }
}
