using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyObject;
    [SerializeField] private Transform Spawner;

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(EnemyObject, Spawner.transform.position, EnemyObject.transform.rotation);
        }
    }
}
