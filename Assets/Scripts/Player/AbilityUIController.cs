using System.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

namespace Player
{
    public class AbilityUIController : MonoBehaviour
    {
        
        [SerializeField]
        private TMP_Text txtAbilityName;
        [SerializeField]
        private TMP_Text txtAbilityStatus;
        [SerializeField]
        private TMP_Text txtAbilityCooldown;

        public void UpdateAbilityName([CanBeNull] string abilityName)
        {
            txtAbilityName.text = abilityName ?? "None";
        }

        public void UpdateAbilityCooldown([CanBeNull] string cooldownNumber)
        {
            txtAbilityCooldown.text = cooldownNumber ?? "0";
        }

        public void UpdateAbilityStatus([CanBeNull] string abilityStatus)
        {
            txtAbilityStatus.text = abilityStatus ?? "Ready";
        }
        
    }
}
