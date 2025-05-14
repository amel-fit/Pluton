using System;
using UnityEngine;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                //not implemented yet
                Debug.Log("Enemy hit! <Not implemented>");
            }
        }
        
    }
}
