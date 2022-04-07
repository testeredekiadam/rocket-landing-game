using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 300f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem turnLeftParticle;
    [SerializeField] ParticleSystem turnRightParticle;
    [SerializeField] ParticleSystem thrustParticle;

    Rigidbody rb;
    AudioSource myAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(mainEngine);
        }
        if (!thrustParticle.isPlaying)
        {
            thrustParticle.Play();
        }
    }

    private void StopThrusting()
    {
        myAudioSource.Stop();
        thrustParticle.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotating();
        }
    }


    private void RotateLeft()
    {
        ApplyRotation(rotateThrust);
        if (!turnLeftParticle.isPlaying)
        {
            turnLeftParticle.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotateThrust);
        if (!turnRightParticle.isPlaying)
        {
            turnRightParticle.Play();
        }
    }

    private void StopRotating()
    {
        turnLeftParticle.Stop();
        turnRightParticle.Stop();
    }

    void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
