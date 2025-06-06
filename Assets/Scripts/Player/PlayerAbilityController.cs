using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge;
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

        private void Start()
        {
            currentStatus = AbilityStatus.Ready;
            inputManager.ActivateAbilityReceived += ActivateAbilityReceived;
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
                currentStatus = AbilityStatus.Active;
                StartCoroutine(AbilityActive());
            }
        }

        private IEnumerator AbilityActive()
        {
            yield return new WaitForSeconds(ability.activeTime);
            Debug.Log("AbilityOnCooldown");
            currentStatus = AbilityStatus.OnCooldown;
            ability.Deactivate(player);
            StartCoroutine(AbilityOnCooldown());
            yield return null;
        }

        private IEnumerator AbilityOnCooldown()
        {
            yield return new WaitForSeconds(ability.cooldown);
            Debug.Log("AbilityReady");
            currentStatus = AbilityStatus.Ready;
            yield return null;
        }
    }
}
