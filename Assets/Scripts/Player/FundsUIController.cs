using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Player
{
    public class FundsUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinAmount; 
        public void UpdateText(string newText)
        {
            coinAmount.text = newText;
        }
    }
}
