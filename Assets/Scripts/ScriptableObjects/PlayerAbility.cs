using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerAbility", menuName = "Scriptable Objects/PlayerAbility")]
    public class PlayerAbility : ScriptableObject
    {
        [SerializeField] public float cooldown;
        [SerializeField] public float activeTime;
        public virtual void Activate(GameObject parent) {}
    }
}
