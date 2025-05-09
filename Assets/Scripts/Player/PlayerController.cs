using System.Collections;
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
        
        [SerializeField] private float dashSpeed = 3;
        [SerializeField] private float  dashTime = 0.25f;
        [SerializeField] private float  dashCooldown = 0.5f;
        
        private bool _isDashing = false;
        private bool _canDash = true;
        
        
        
        [SerializeField]
        private CharacterController controller;
        
        private Animator animator;
        
        
        private void Start()
        {
            Movement.MovementInputReceived += MovementInputReceived;
            Movement.DashInputReceived += DashInputReceived;
            animator = GetComponent<Animator>();
        }

        private void DashInputReceived(bool doDash)
        {
            if (doDash && _canDash)
                StartCoroutine(Dash());
        }

        private IEnumerator Dash()
        {
            _canDash = false;
            
            var startTime = Time.time;
            _isDashing = true;
            while (Time.time < startTime + dashTime)
            {
                //do the dash, dash speed compounds on regular speed so it doesn't have to be bigger than moveSpeed
                controller.Move(direction * (dashSpeed * Time.fixedDeltaTime));
                yield return null;
            }
            _isDashing = false;
            
            //Activate cooldown
            var cooldown = new WaitForSeconds(dashCooldown);
            yield return cooldown;
            _canDash = true;
        }
        
        private void MovementInputReceived(float horizontal, float vertical)
        {
            direction = new Vector3(horizontal, 0, vertical).normalized;
            
            RotatePlayer(horizontal, vertical);
            if (!_isDashing)
                MovePlayer(direction * (Time.fixedDeltaTime * speed));
            
        }

        private void RotatePlayer(float horizontal, float vertical)
        {
            
            if (horizontal != 0 && vertical != 0)
            {
                //45 degrees
                transform.rotation = Quaternion.Euler(new Vector3(0,1,0) * ((45 + ((vertical > 0)? 0: 90)) * horizontal));
            }else if (horizontal != 0)
            {
                //left or right
                transform.rotation = (Quaternion.Euler(Vector3.up * (90 * horizontal)));
            }else if (vertical != 0)
            {
                //up or down
                transform.rotation = (Quaternion.Euler(Vector3.up * (180 * ((vertical > 0) ? 0 : 1)) ));
            }
        }

        private void MovePlayer(Vector3 directionAndSpeed)
        {
            //Debug.Log(horizontal + " " + vertical);
            //Debug.Log($"{direction.x} {direction.y} {direction.z}");
            controller.Move(directionAndSpeed);
            if (directionAndSpeed != Vector3.zero)
                animator.SetFloat("Speed", 1f);
            else
                animator.SetFloat("Speed", 0f);
        }
    }
}
