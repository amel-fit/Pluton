using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge;
using Codice.CM.Common.Tree;
using Management;
using PlasticPipe.Server;
using ScriptableObjects;
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
        private AbilityStatus currentStatus;

        [SerializeField]
        private AbilityUIController abilityUIController;
        private void Start()
        {
            ChangeStatus(AbilityStatus.Ready);
            inputManager.ActivateAbilityReceived += ActivateAbilityReceived;
            abilityUIController.UpdateAbilityName(ability.name);
            
            abilityUIController.UpdateAbilityCooldown("0");
        }

        private void ActivateAbilityReceived(bool doActivate)
        {
            if (doActivate)
            {
                //Debug.Log("ActivateAbilityInputReveived");
                ActivateAbility();
            }
        }

        private void ActivateAbility()
        {
            if (currentStatus == AbilityStatus.Ready)
            {
                Debug.Log("ActivatedAbility");
                ability.Activate(player);
                ChangeStatus(AbilityStatus.Active);
                
                StartCoroutine(AbilityActive());
            }
        }

        private IEnumerator AbilityActive()
        {
            //StartCoroutine(CountDown(ability.activeTime));
            yield return new WaitForSeconds(ability.activeTime);
            Debug.Log("AbilityOnCooldown");
            ChangeStatus(AbilityStatus.OnCooldown);
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
            while (currentStatus != AbilityStatus.Ready)
            {
                yield return new WaitForSeconds(1);
            }

            ability = newAbility;
            abilityUIController.UpdateAbilityName(ability.name);
        }

        private void ChangeStatus(AbilityStatus newStatus)
        {
            currentStatus = newStatus;
            abilityUIController.UpdateAbilityStatus(newStatus.ToString());
        }
        
        private IEnumerator CountDown(float abilityActiveTime)
        {
            float startTime = Time.time;
            while (Time.time < startTime + abilityActiveTime)
            {
                abilityUIController.UpdateAbilityCooldown((abilityActiveTime - Time.deltaTime).ToString());
            }
            abilityUIController.UpdateAbilityCooldown("0");
            yield return null;
        }
    }
}
