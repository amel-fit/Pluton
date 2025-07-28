using ScriptableObjects;
using UnityEngine;

namespace  Enemy
{
    public class EnemyWeaponController : MonoBehaviour
    {
        private Transform playerTransform;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Hit");
            }
        }
        
    }
}
