using System.Collections;
using System.Collections.Generic;
using Management;
using UnityEditor;
using UnityEngine;

namespace PlayerController.Player
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        private InputManager Movement;
        
        [Header("Movement")]
        [SerializeField] private float speed; //scriptable
        private bool isMoving = false;
        private Vector3 direction = Vector3.zero;
        private bool suspendMovement = false;
        
        [Header("Dashing")]
        
        [SerializeField] private float dashSpeed = 3; //scriptable
        [SerializeField] private float  dashTime = 0.25f; //scriptable
        [SerializeField] private float  dashCooldown = 0.5f; //scriptable
        
        private bool _isDashing = false;
        private bool _canDash = true;


        [Header("Attacking")]
        
        [SerializeField] private float attackCooldown = 1; //scriptable
        private bool isAttacking = false;
        private bool canAttack = true;
        
        
        [SerializeField]
        private CharacterController controller;


        [Header("Combat")]
        [SerializeField] private Collider weaponCollider;

        [SerializeField] private List<GameObject> weapons;
        private int currentWeapon = 0;
        [SerializeField] private List<int> weaponAnimationHashes;
        
        [Header("Animation")]
        private Animator animator;


        private static readonly int IsDashingAnim = Animator.StringToHash("isDashingAnim");
        private static readonly int Attack1 = Animator.StringToHash("Attack1");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        private static readonly int OneHandedIdle = Animator.StringToHash("Idle");
        private static readonly int TwoHandedIdle = Animator.StringToHash("2H_Idle");


        private void Start()
        {
            Movement.MovementInputReceived += MovementInputReceived;
            Movement.DashInputReceived += DashInputReceived;
            Movement.AttackInputReceived += AttackInputReceived;
            
            animator = GetComponent<Animator>();
            weaponAnimationHashes.Add(OneHandedIdle);
            weaponAnimationHashes.Add(TwoHandedIdle);
            Movement.WeaponSwitchInputReceived += WeaponSwitchInputReceived;

        }

        private void WeaponSwitchInputReceived(int weaponIndex)
        {
            switch (weaponIndex)
            {
                case 1:
                    if(currentWeapon != 0)
                        SwitchWeapon1();
                    break;
                case 2:
                    if(currentWeapon != 1)
                        SwitchWeapon2();
                    break;
            }
        }

        private void SwitchWeapon2()
        {
            weapons[1].SetActive(true);
            weapons[0].SetActive(false);
            animator.Play(TwoHandedIdle);
            currentWeapon = 1;
        }

        private void SwitchWeapon1()
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            animator.Play(OneHandedIdle);
            currentWeapon = 0;
        }

        private void AttackInputReceived(bool doAttack)
        {
            if (doAttack && canAttack)
            {
                Attack();
            }
        }

        private void Attack()
        {
            animator.SetTrigger(Attack1);
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
            animator.SetBool(IsDashingAnim, true);
            while (Time.time < startTime + dashTime)
            {
                //do the dash, dash speed compounds on regular speed so it doesn't have to be bigger than moveSpeed
                controller.Move(direction * (dashSpeed * Time.fixedDeltaTime));
                yield return null;
            }
            _isDashing = false;
            animator.SetBool(IsDashingAnim, false);
            
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
            isMoving = directionAndSpeed != Vector3.zero;
            if (isMoving && !suspendMovement)
            {
                controller.Move(directionAndSpeed);
                animator.SetFloat(Speed, 1f);
            }
            else
                animator.SetFloat(Speed, 0f);
        }


        //[Header("Collision Toggles")]
        private void SwordCollisionOn()
        {
            //weaponCollider.enabled = true;
            weapons[currentWeapon].GetComponent<Collider>().enabled = true;
        }

        private void SwordCollisionOff()
        {
            //weaponCollider.enabled = false;
            weapons[currentWeapon].GetComponent<Collider>().enabled = false;
        }

        private void SuspendMovementOn()
        {
            suspendMovement = true;
        }

        private void SuspendMovementOff()
        {
            suspendMovement = false;
        }
        
    }
}
