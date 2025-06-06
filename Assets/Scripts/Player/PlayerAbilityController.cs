using System;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerAbilityController : MonoBehaviour
    {
        public PlayerAbility ability;
        [SerializeField] GameObject player;
        
        private void Update()
        {
            // if (!active)
            // {
            //     //have to wait for active time here and not in ability script....fml
            //     active = true;
            //     ability.Activate(player);
            //     active = false;
            // }
        }
    }
}
