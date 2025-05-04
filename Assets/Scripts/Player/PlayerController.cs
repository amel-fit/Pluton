using System;
using Management;
using UnityEngine;

namespace PlayerController.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private InputManager Movement;

        [SerializeField] private float speed;
        private Vector3 direction = Vector3.zero;
        
        [Header("Dashing")]
        [SerializeField] private int dashSpeed;
        
        
        
        [SerializeField]
        private Rigidbody playerRigidBody;
        
        
        private void Start()
        {
            Movement.MovementInputReceived += MovementInputReceived;
            //Movement.DashInputReceived += DashInputReceived;
            //playerRigidBody.rotation = Quaternion.Euler(0,-90,0);
        }

        

        private void MovementInputReceived(float horizontal, float vertical, bool doDash)
        {
            MovePlayer(horizontal, vertical, doDash);

            RotatePlayer(horizontal, vertical);

            direction = Vector3.zero;
        }

        private void RotatePlayer(float horizontal, float vertical)
        {
            
            if (horizontal != 0 && vertical != 0)
            {
                //45 degrees
                // Debug.Log($"H: {horizontal}");
                // Debug.Log($"V: {vertical}");
                playerRigidBody.MoveRotation(Quaternion.Euler(new Vector3(0,1,0) * (45 + ((vertical > 0)? 0: 90)) * horizontal));
            }else if (horizontal != 0)
            {
                //left or right
                playerRigidBody.MoveRotation(Quaternion.Euler(Vector3.up * 90 * horizontal));
            }else if (vertical != 0)
            {
                //up or down
                playerRigidBody.MoveRotation(Quaternion.Euler(Vector3.up * 180 * ((vertical > 0) ? 0 : 1) ));
            }
        }

        private void MovePlayer(float horizontal, float vertical, bool doDash)
        {
            //Debug.Log(horizontal + " " + vertical);
            direction = new Vector3(horizontal, 0, vertical) * Time.fixedDeltaTime * (speed);
            
            playerRigidBody.MovePosition(new Vector3(this.playerRigidBody.position.x + direction.x, this.playerRigidBody.position.y, this.playerRigidBody.position.z + direction.z));
        }
    }
}
