using System;
using Core;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace GameData.PlayerAbilities
{
    [CreateAssetMenu(fileName = "Thorns", menuName = "Scriptable Objects/Thorns")]
    public class Thorns : PlayerAbilityData
    {
        private PlayerController.Player.PlayerController playerController = null;
        
        public override void Activate(GameObject player)
        {
            //When activated, make it so that when the player takes damage, the collider that did the damage also takes damage
            if(playerController == null)
                playerController = player.GetComponent<PlayerController.Player.PlayerController>();
            playerController.DamageTaken += DamageTheSource;
            Debug.Log("THORNS ON");
        }

        public override void Deactivate(GameObject player)
        {
            playerController.DamageTaken -= DamageTheSource;
            Debug.Log("THORNS OFF");
        }

        public void DamageTheSource(float damage, IDamageable source)
        {
            source.TakeDamage(damage / 2, playerController);
        }
        
    }
}
