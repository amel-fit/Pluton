using System;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PickupManager : MonoBehaviour
    {
        [SerializeField] private PlayerAbilityData HeldAbility = null;
        //I have decided that there will be no interface for various pickups, just one pickup class that handles all of it
        [SerializeField] private float HealAmount = 0;
        [SerializeField] private float MaxHealthIncrease = 0;
        [SerializeField] private int Cost = 0;
        private void OnCollisionEnter(Collision other)
        {
            //other == player because of layer interaction settings in project
            //Debug.Log("Collision enter");
            
            if (!other.collider.CompareTag("Player")) return;  
            
            var playerMain = other.collider.GetComponent<PlayerController>();
            if (!playerMain.DecreaseFunds(Cost)) 
                return;
            playerMain.IncreaseMaxHealth(MaxHealthIncrease);
            playerMain.Heal(HealAmount);
            
            var playerAbility = other.collider.GetComponent<PlayerAbilityController>();
            playerAbility.SetAbility(HeldAbility);
            
            
            gameObject.SetActive(false);
            
        }

        private void OnCollisionStay(Collision other)
        {
            Debug.Log("Staying");
        }
    }
}
