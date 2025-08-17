using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

namespace Player
{
    public class PlayerSoundController : MonoBehaviour
    {
        private AudioSource playerAudioSource;
        [SerializeField] private float fadeSpeed = 5f;
        [SerializeField ]private float targetVolume = 0f;
        private void Start()
        {
            playerAudioSource = GetComponent<AudioSource>();
            playerAudioSource.volume = 0f;
            playerAudioSource.Play();
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            bool isMoving = horizontal != 0 || vertical != 0;

            targetVolume = isMoving ? 0.2f : 0f;
            playerAudioSource.volume = Mathf.Lerp(playerAudioSource.volume, targetVolume, Time.deltaTime * fadeSpeed);
        }
    }
    
}

