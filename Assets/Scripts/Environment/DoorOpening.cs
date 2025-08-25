using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [SerializeField] private Transform Enemies;
    [SerializeField] private float angle;
    void Update()
    {
        if (Enemies.childCount == 0)
        {
            var opened = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, opened, 40f * Time.deltaTime);
        }
    }
}
