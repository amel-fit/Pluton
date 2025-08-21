using System;
using System.Collections;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PickupManager : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private PlayerAbilityData HeldAbility = null;
        //I have decided that there will be no interface for various pickups, just one pickup class that handles all of it
        [SerializeField] private float HealAmount = 0;
        [SerializeField] private float MaxHealthIncrease = 0;
        [SerializeField] private int Cost = 0;
        
        private bool flag = false;
        private Collider playerCollider;

        [SerializeField] private GameObject promptUI;
        [SerializeField] private TextMeshProUGUI text;
        private void OnCollisionEnter(Collision other)
        {
            //other == player because of layer interaction settings in project
            //Debug.Log("Collision enter");
            
            if (!other.collider.CompareTag("Player")) return;  
            
            var playerMain = other.collider.GetComponent<PlayerController>();
            if (!playerMain.DecreaseFunds(Cost)) 
                return;
            playerMain.IncreaseMaxHealth(MaxHealthIncrease);
            playerMain.Heal(HealAmount);
            
            var playerAbility = other.collider.GetComponent<PlayerAbilityController>();
            playerAbility.SetAbility(HeldAbility);
            
            
            gameObject.SetActive(false);
            Destroy(gameObject);

        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                flag = true;
                promptUI.SetActive(true);
                text.text = $"Press E to buy {itemName} for {Cost} coins";
                playerCollider = other;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                flag = false;
                promptUI.SetActive(false);
            }
        }

        private void Update()
        {
            if (flag && Input.GetKeyDown(KeyCode.E))
            {
                BuyItem();
            }
        }

        private void BuyItem()
        {
            var playerMain = playerCollider.GetComponent<PlayerController>();
            if (!playerMain.DecreaseFunds(Cost)) 
                return;
            playerMain.IncreaseMaxHealth(MaxHealthIncrease);
            playerMain.Heal(10);
                
            var playerAbility = playerCollider.GetComponent<PlayerAbilityController>();
            playerAbility.SetAbility(HeldAbility);
            
            gameObject.SetActive(false);
            Destroy(gameObject);
            promptUI.SetActive(false);
        }
    }
}
