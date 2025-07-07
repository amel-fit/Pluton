using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class HealthUIController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private Image healthUIImage;

        public void UpdateImageFill(float current, float max)
        {
            healthUIImage.fillAmount = (current / max);
        }
    }
}
