using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge;
using Codice.CM.Common.Tree;
using Management;
using PlasticPipe.Server;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    enum AbilityStatus {Ready, Active, OnCooldown}
    public class PlayerAbilityController : MonoBehaviour
    {
        [SerializeField] public InputManager inputManager;
        [SerializeField] public PlayerAbilityData ability;
        [SerializeField] GameObject player;
        private AbilityStatus _currentStatus;
        
        private float _countDownTime;
        private float _statusChangeTime;

        [SerializeField]
        private AbilityUIController abilityUIController;
        private void Start()
        {
            ChangeStatus(AbilityStatus.Ready);
            inputManager.ActivateAbilityReceived += ActivateAbilityReceived;
            abilityUIController.UpdateAbilityName(ability.name);
            
            abilityUIController.UpdateAbilityCooldown("0");
        }

        private void Update()
        {
            /*counting down here because it's easier than making a coroutine plus that didn't work so...?*/
            if (_currentStatus != AbilityStatus.Ready)
            {
                {
                    if (_countDownTime > _statusChangeTime)
                    {
                        _countDownTime -= Time.deltaTime;
                        abilityUIController.UpdateAbilityCooldown((_countDownTime - _statusChangeTime).ToString("F2"));
                    }
                }
            }else
                abilityUIController.UpdateAbilityCooldown(null);
                
        }

        private void ActivateAbilityReceived(bool doActivate)
        {
            if (doActivate)
            {
                //Debug.Log("ActivateAbilityInputReceived");
                ActivateAbility();
            }
        }

        private void ActivateAbility()
        {
            if (_currentStatus == AbilityStatus.Ready)
            {
                Debug.Log("ActivatedAbility");
                ability.Activate(player);
                ChangeStatus(AbilityStatus.Active);
                _countDownTime = Time.time + ability.activeTime;
                StartCoroutine(AbilityActive());
            }
        }

        private IEnumerator AbilityActive()
        {
            //StartCoroutine(CountDown(ability.activeTime));
            yield return new WaitForSeconds(ability.activeTime);
            Debug.Log("AbilityOnCooldown");
            ChangeStatus(AbilityStatus.OnCooldown);
            _countDownTime = _statusChangeTime + ability.cooldown;
            ability.Deactivate(player);
            StartCoroutine(AbilityOnCooldown());
            yield return null;
        }

        

        private IEnumerator AbilityOnCooldown()
        {
            
            //StartCoroutine(CountDown(ability.cooldown));
            yield return new WaitForSeconds(ability.cooldown);
            Debug.Log("AbilityReady");
            ChangeStatus(AbilityStatus.Ready);
            yield return null;
        }

        public void SetAbility(PlayerAbilityData newAbility)
        {
            //don't.    
            //while (currentStatus != AbilityStatus.Ready) ;
            //ability = newAbility;
            if(newAbility != null)
                StartCoroutine(WaitAndSet(newAbility));    
        }

        private IEnumerator WaitAndSet(PlayerAbilityData newAbility)
        {
            while (_currentStatus != AbilityStatus.Ready)
            {
                yield return new WaitForSeconds(1);
            }

            ability = newAbility;
            abilityUIController.UpdateAbilityName(ability.name);
        }

        private void ChangeStatus(AbilityStatus newStatus)
        {
            _statusChangeTime = Time.time;
            _currentStatus = newStatus;
            abilityUIController.UpdateAbilityStatus(newStatus.ToString());
        }
        
    }
}
