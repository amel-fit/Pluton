using Core;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerAbility", menuName = "Scriptable Objects/PlayerAbility")]
    public class PlayerAbilityData : ScriptableObject
    {
        [SerializeField] public float cooldown;
        [SerializeField] public float activeTime;
        [SerializeField] public string name;
        public virtual void Activate(GameObject player) {}
        
        public virtual void Deactivate(GameObject player) { }

        
    }
}
