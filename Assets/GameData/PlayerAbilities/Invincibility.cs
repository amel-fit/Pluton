using ScriptableObjects;
using UnityEngine;

namespace GameData.PlayerAbilities
{
    [CreateAssetMenu(fileName = "Invincibility", menuName = "Scriptable Objects/Invincibility")]
    public class Invincibility : PlayerAbility
    {
        public override void Activate(GameObject parent)
        {
            // Collider c = parent.GetComponent<Collider>();
            // c.enabled = false;
            // Debug.Log("INVINCIBLE!");
                //wait?
            // c.enabled = true;
            // Debug.Log("vincible....");
        }
    
    }
}
