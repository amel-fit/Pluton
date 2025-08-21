using System;
using Player;
using TMPro;
using UnityEngine;

namespace Player
{
    public class ItemShopManager : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] int itemPrice;
        private bool flag = false;

        [SerializeField] private GameObject promptUI;
        [SerializeField] private TextMeshProUGUI text;

        private Collider playerCollider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                flag = true;
                promptUI.SetActive(true);
                text.text = $"Press E to buy {itemName} for {itemPrice}";
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
            if (!playerMain.DecreaseFunds(itemPrice)) 
                return;
            //playerMain.IncreaseMaxHealth(MaxHealthIncrease);
            playerMain.Heal(10);
                
            //var playerAbility = other.collider.GetComponent<PlayerAbilityController>();
            //playerAbility.SetAbility(HeldAbility);
        }
    }
    
}
