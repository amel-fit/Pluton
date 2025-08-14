using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform EnemyParent;
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player") && EnemyParent.childCount == 0)
        {
            Debug.Log("works");
            GetComponent<Collider>().enabled = false;
        }
    }
    
    
}
