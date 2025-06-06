using System;
using Codice.Client.Common.OAuth;
using UnityEngine;

namespace Management
{
    public class InputManager : MonoBehaviour
    {
        public Action<float, float> MovementInputReceived;
        public Action<bool> DashInputReceived;
        public Action<bool> AttackInputReceived;
        public Action<int> WeaponSwitchInputReceived;
        public Action<bool> ActivateAbilityReceived;
        

        public void FixedUpdate()
        {
            MovementInputReceived?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        public void Update()
        {
            //Has to be in update because FixedUpdate doesn't pick up on KeyDown consistently 
            DashInputReceived?.Invoke(Input.GetKeyDown(KeyCode.LeftShift));
            AttackInputReceived?.Invoke(Input.GetKeyDown(KeyCode.Mouse0));
            if (Input.GetKeyDown(KeyCode.Alpha1)) WeaponSwitchInputReceived?.Invoke(1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) WeaponSwitchInputReceived?.Invoke(2);
            ActivateAbilityReceived?.Invoke(Input.GetKeyDown(KeyCode.Q));
            
        }
    }
}
