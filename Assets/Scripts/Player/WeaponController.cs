using System;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit! <Not implemented>");
            }
        }
        
    }
}
