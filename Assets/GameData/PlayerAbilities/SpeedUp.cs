using Core;
using ScriptableObjects;
using UnityEngine;

namespace GameData.PlayerAbilities
{
    [CreateAssetMenu(fileName = "SpeedUp", menuName = "Scriptable Objects/SpeedUp")]
    public class SpeedUp : PlayerAbilityData
    {
        private float oldSpeed = 0; 
        private float newSpeed = 0;
        public override void Activate(GameObject player)
        {
            oldSpeed = player.GetComponent<PlayerController.Player.PlayerController>().speed;
            newSpeed = oldSpeed * 1.2f;
            //Debug.Log($"{oldSpeed} -> {newSpeed}");
            player.GetComponent<PlayerController.Player.PlayerController>().speed = newSpeed;
        }

        public override void Deactivate(GameObject player)
        {
            //Debug.Log($"{newSpeed} -> {oldSpeed}");
            player.GetComponent<PlayerController.Player.PlayerController>().speed = oldSpeed;
            oldSpeed = 0;
            newSpeed = 0;
        }
    }
}
