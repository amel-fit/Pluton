using System.Collections;
using System.Text;
using Core;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private float loweredY = -1.6f;
    private float raisedY = -0.8f;
    [SerializeField] private float spikeSpeed = 5f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float pullBackTime = 4f;
    private bool isMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            if (transform.position.y < loweredY)
            {
                StartCoroutine(SpikeMovement(other));
            }
        }
    }

    private IEnumerator SpikeMovement(Collider other)
    {
        isMoving = true;

        yield return new WaitForSeconds(waitTime);

        yield return SpikeMove(raisedY);
        transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        
        yield return new WaitForSeconds(pullBackTime);
        transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        yield return SpikeMove(-1.67f);
        isMoving = false;
    }

    private IEnumerator SpikeMove(float height)
    {
        Vector3 newPosition = new Vector3(transform.position.x, height, transform.position.z);

        while (Vector3.Distance(transform.position, newPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, spikeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
