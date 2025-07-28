using System;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject Room1_Lights;
    public GameObject Room2_Lights;
    public GameObject[] Room1_torches;
    public GameObject[] Room2_torches;
    
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (Room1_Lights.activeSelf == true)
            {
                Room1_Lights.SetActive(false);
                Room2_Lights.SetActive(true);
                foreach (var torch in Room2_torches)
                {
                    torch.transform.GetChild(0).gameObject.SetActive(true);
                }
                foreach (var torch in Room1_torches)
                {
                    torch.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                Room2_Lights.SetActive(false);
                Room1_Lights.SetActive(true);
                foreach (var torch in Room2_torches)
                {
                    torch.transform.GetChild(0).gameObject.SetActive(false);
                }
                foreach (var torch in Room1_torches)
                {
                    torch.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
}
