using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpening : MonoBehaviour
{
    [SerializeField] private Transform Enemies;
    [SerializeField] private Transform EnemiesLast;
    [SerializeField] private float angle;
    void Update()
    {
        if (Enemies.childCount == 0)
        {
            OpenDoors();
        }
        if (EnemiesLast.childCount == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OpenDoors()
    {
        var opened = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, opened, 40f * Time.deltaTime);
    }
}
