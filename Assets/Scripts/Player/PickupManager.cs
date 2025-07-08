using System;
using Core;
using Player;
using ScriptableObjects;
using UnityEngine;

namespace Management
{
    public class PickupManager : MonoBehaviour
    {
        [SerializeField] private PlayerAbilityData HeldAbility;
        [SerializeField]
        private void OnCollisionEnter(Collision other)
        {
            //other == player because of layer interaction settings in project
            Debug.Log("Collision enter");
            var player = other.collider.GetComponent<PlayerAbilityController>();
            player.SetAbility(HeldAbility); 
            gameObject.SetActive(false);
            
        }
        
        
    }
}
