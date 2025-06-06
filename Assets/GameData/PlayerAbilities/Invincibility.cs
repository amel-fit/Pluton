using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace GameData.PlayerAbilities
{
    [CreateAssetMenu(fileName = "Invincibility", menuName = "Scriptable Objects/Invincibility")]
    public class Invincibility : PlayerAbilityData
    {
        private Collider playerCollider = new();
        public override void Activate(GameObject player)
        {
            playerCollider = player.GetComponent<PlayerController.Player.PlayerController>().hitBox;
            playerCollider.enabled = false;
            Debug.Log("INVINCIBLE!");
        }

        public override void Deactivate(GameObject player)
        {
            playerCollider.enabled = true;
            Debug.Log("vincible....");
        }
    }
}
