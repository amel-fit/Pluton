using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerAbility", menuName = "Scriptable Objects/PlayerAbility")]
    public class PlayerAbilityData : ScriptableObject
    {
        [SerializeField] public float cooldown;
        [SerializeField] public float activeTime;
        public virtual void Activate(GameObject player) {}

        public virtual void Deactivate(GameObject player) { }
    }
}
