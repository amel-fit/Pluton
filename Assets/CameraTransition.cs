using Unity.Cinemachine;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public CinemachineCamera cameraToActivate;
    public CinemachineCamera cameraToDeactivate;
    private bool flag = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (flag == false)
            {
                cameraToActivate.Priority = 20;
                cameraToDeactivate.Priority = 10;
                flag = true;
            }
            else
            {
                cameraToActivate.Priority = 10;
                cameraToDeactivate.Priority = 20;
                flag = false;
            }
        }
    }
}
