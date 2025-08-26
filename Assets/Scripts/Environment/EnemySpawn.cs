using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> EnemyObjects;
    [SerializeField] private List<Transform> Spawners;
    [SerializeField] private Transform Parent;
    void Start()
    {
        for (int i = 0; i < Spawners.Count; i++)
        {
            Instantiate(EnemyObjects[i],Spawners[i].transform.position,EnemyObjects[i].transform.rotation,Parent);
        }
    }

}
