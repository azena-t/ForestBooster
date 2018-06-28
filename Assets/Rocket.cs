﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] AudioClip mainEngineAudioClip;
    [SerializeField] AudioClip successAudioClip;
    [SerializeField] AudioClip deathAudioClip;
    [SerializeField] ParticleSystem mainEngineParticleSystem;
    [SerializeField] ParticleSystem successParticleSystem;
    [SerializeField] ParticleSystem deathParticleSystem;

    enum State
    {
        ALIVE,
        DYING,
        TRANSCENDING
    }

    private State state = State.ALIVE;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.ALIVE)
        {
            Thrust();
            Rotate();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (state != State.ALIVE)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                Die();
                break;
        }
    }

    private void Die()
    {
        state = State.DYING;
        audioSource.Stop();
        deathParticleSystem.Play();
        audioSource.PlayOneShot(deathAudioClip);
        Invoke("ResetGame", 1f);
    }

    private void LoadNextLevel()
    {
        audioSource.Stop();
        successParticleSystem.Play();
        audioSource.PlayOneShot(successAudioClip);
        state = State.TRANSCENDING;
        Invoke("LoadNextScene", 1f);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustThisFrame = mainThrust * Time.deltaTime;
            if (!mainEngineParticleSystem.isPlaying)
            {
                mainEngineParticleSystem.Play();
            }

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineAudioClip);
            }

            rigidbody.AddRelativeForce(Vector3.up * thrustThisFrame);
        }
        else
        {
            mainEngineParticleSystem.Stop();
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}