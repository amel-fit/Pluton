using System.Collections;
using System.Collections.Generic;
using Core;
using Management;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerController.Player
{
    enum WeaponIndex {Sword = 0, Axe = 1}
    public class PlayerController : MonoBehaviour, IEntity, IDamageable
    {
        [FormerlySerializedAs("Movement")] [SerializeField]
        private InputManager inputManager;
        [SerializeField]
        private CharacterController controller;
        
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
        private bool canAttack = true;
        private bool midAxeAnimation = false;
        
        [Header("Combat")]
        [SerializeField] private List<GameObject> weapons;
        [SerializeField] private List<int> weaponAnimationHashes;
        private WeaponIndex _currentWeaponIndex = 0;
        [SerializeField] public Collider hitBox = null;
        
        [field: SerializeField]
        public CharacterCharacteristics Characteristics { get; set; }
        
        [SerializeField]
        private CharacterCharacteristicsData CharacteristicsData;
        
        [field: SerializeField]
        public float Health { get; set; }

        public void TakeDamage()
        {
            throw new System.NotImplementedException();
        }


        [Header("Animation")]
        private Animator animator;

        private static readonly int IsDashingAnim = Animator.StringToHash("isDashingAnim");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        private static readonly int Attack1 = Animator.StringToHash("Attack1");
        private static readonly int OneHandedIdle = Animator.StringToHash("Idle");
        private static readonly int TwoHandedIdle = Animator.StringToHash("2H_Idle");

        private void Start()
        {
            inputManager.MovementInputReceived += MovementInputReceived;
            inputManager.DashInputReceived += DashInputReceived;
            inputManager.AttackInputReceived += AttackInputReceived;
            inputManager.WeaponSwitchInputReceived += WeaponSwitchInputReceived;
            
            animator = GetComponent<Animator>();
            weaponAnimationHashes.Add(OneHandedIdle);
            weaponAnimationHashes.Add(TwoHandedIdle);

            Characteristics = new CharacterCharacteristics()
            {
                Dexterity = CharacteristicsData.characteristics.Dexterity,
                Strength = CharacteristicsData.characteristics.Strength,
                StartingHealth = CharacteristicsData.characteristics.StartingHealth
            };
            
            Health = Characteristics.StartingHealth;
        }

        #region WeaponSwitching
        private void WeaponSwitchInputReceived(int weaponIndex)
        {
            if (midAxeAnimation) return;
            switch (weaponIndex)
            {
                case 1:
                    if(_currentWeaponIndex != WeaponIndex.Sword)
                        SwitchToWeaponSword();
                    break;
                case 2:
                    if(_currentWeaponIndex != WeaponIndex.Axe)
                        SwitchToWeaponAxe();
                    break;
            }
        }

        private void SwitchToWeaponAxe()
        {
            animator.Play(TwoHandedIdle);
            weapons[(int)WeaponIndex.Axe].SetActive(true);
            weapons[(int)WeaponIndex.Sword].SetActive(false);
            _currentWeaponIndex = WeaponIndex.Axe;
        }

        private void SwitchToWeaponSword()
        {
            animator.Play(OneHandedIdle);
            weapons[(int)WeaponIndex.Sword].SetActive(true);
            weapons[(int)WeaponIndex.Axe].SetActive(false);
            _currentWeaponIndex = WeaponIndex.Sword;
        }
        #endregion

        #region Attacking

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
        #endregion

        #region Abilities

        

        #endregion
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

        #region Movement
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
            
            isMoving = directionAndSpeed != Vector3.zero;
            if (isMoving && !suspendMovement)
            {
                controller.Move(directionAndSpeed);
                animator.SetFloat(Speed, 1f);
            }
            else
            {
                animator.SetFloat(Speed, 0f);
            }
        }
        #endregion

        #region AnimationEvents
        private void WeaponCollisionOn() => weapons[(int)_currentWeaponIndex].GetComponent<Collider>().enabled = true;
        private void WeaponCollisionOff() => weapons[(int)_currentWeaponIndex].GetComponent<Collider>().enabled = false;
        private void AxeAttackStart() => midAxeAnimation = true;
        private void AxeAttackEnd() => midAxeAnimation = false;
        private void SuspendMovementOn() => suspendMovement = true;
        private void SuspendMovementOff() => suspendMovement = false;
        #endregion
    }
}
