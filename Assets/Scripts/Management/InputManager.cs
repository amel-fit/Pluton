using System;
using UnityEngine;

namespace Management
{
    public class InputManager : MonoBehaviour
    {
        public Action<float, float> MovementInputReceived;
        public Action<bool> DashInputReceived; 

        public void FixedUpdate()
        {
            MovementInputReceived?.Invoke(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        public void Update()
        {
            //Has to be in update because FixedUpdate doesn't pick up on KeyDown consistently 
            DashInputReceived?.Invoke(Input.GetKeyDown(KeyCode.LeftShift));
        }
    }
}
