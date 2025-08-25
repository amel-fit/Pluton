using System;
using System.Collections.Generic;
//using Codice.Client.Common.OAuth;
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

        public static Dictionary<string, KeyCode> KeyBinds = new()
        {
            { "Dash", KeyCode.LeftShift },
            { "Attack", KeyCode.J },
            { "Weapon1", KeyCode.Alpha1 },
            { "Weapon2", KeyCode.Alpha2 },
            { "Ability", KeyCode.Q }
        };
            

        public void FixedUpdate()
        {
            MovementInputReceived?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        public void Update()
        {
            //Has to be in update because FixedUpdate doesn't pick up on KeyDown consistently 
            DashInputReceived?.Invoke(Input.GetKeyDown(KeyBinds["Dash"]));
            AttackInputReceived?.Invoke(Input.GetKeyDown(KeyBinds["Attack"]));
            if (Input.GetKeyDown(KeyBinds["Weapon1"])) WeaponSwitchInputReceived?.Invoke(1);
            if (Input.GetKeyDown(KeyBinds["Weapon2"])) WeaponSwitchInputReceived?.Invoke(2);
            ActivateAbilityReceived?.Invoke(Input.GetKeyDown(KeyBinds["Ability"]));
            
        }
    }
}
