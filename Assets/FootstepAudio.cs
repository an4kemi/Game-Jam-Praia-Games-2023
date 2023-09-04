using System;
using StarterAssets;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootstepAudio : MonoBehaviour
{
    public FirstPersonController _controller;
    public AudioSource audioSource;
    public AudioClip walkSound;
    public float footstepDelay;

    private float nextFootstep = 0;

    private void Awake()
    {
        _controller = GetComponent<FirstPersonController>();
    }

    void Update () {
        if (!_controller.Grounded) return;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)
                                    || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) {
            nextFootstep -= Time.deltaTime;
            
            if (nextFootstep <= 0)
            {
                audioSource.pitch = Random.Range(0.7f, 1.3f);
                audioSource.PlayOneShot(walkSound, 0.7f);
                var isRunning = Input.GetKey(KeyCode.LeftShift);
                nextFootstep += isRunning ? footstepDelay * .75f : footstepDelay;
            }
        }
    }
}
