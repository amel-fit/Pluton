using System;
using Player;
using ScriptableObjects;
using UnityEngine;

namespace Management
{
    public class PickupManager : MonoBehaviour
    {
        [SerializeField] private PlayerAbilityData HeldAbility;
        private void OnCollisionEnter(Collision other)
        {
            //other == player because of layer interaction settings in project
            Debug.Log("Collision enter");
            var player = other.collider.GetComponent<PlayerAbilityController>();
            player.ability = HeldAbility;
        }

        private void OnCollisionExit(Collision other)
        {
            Debug.Log("Collision exit");
            //throw new NotImplementedException();
        }

        private void OnCollisionStay(Collision other)
        {
            Debug.Log("Collision stay?");
        }
    }
}
