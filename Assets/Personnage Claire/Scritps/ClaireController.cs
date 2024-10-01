using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaireController : MonoBehaviour
{

    Animator claireAnimator;
    AudioSource claireAudioSource;
    CapsuleCollider claireCapsule;

    [SerializeField]
    float walkSpeed = 2f, runSpeed = 8f, rotSpeed = 10f, jumpForce = 350f;

    Rigidbody rb;

    const float timeout = 15.0f;
    [SerializeField] float countdown = timeout;

    [SerializeField] AudioClip sndJump, sndImpact, sndLeftFoot, sndRightFoot;
    bool switchFoot = false;

    [SerializeField] bool isJumping = false;

    private void Awake()
    {
        claireAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        claireAudioSource = GetComponent<AudioSource>();
        claireCapsule = GetComponent<CapsuleCollider>();
    }

    void Update()
    {

        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");

        // Movement
        Vector3 movement = new Vector3(0, 0, axisV).normalized;
        movement = transform.TransformDirection(movement); // Move relative to the character's facing direction

        // Walking/Running logic
        if (axisV != 0)
        {
            float currentSpeed = Input.GetKey(KeyCode.LeftControl) ? runSpeed : walkSpeed;
            transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

            if (axisV > 0)
            {
                claireAnimator.SetBool("walk", !Input.GetKey(KeyCode.LeftControl));
                claireAnimator.SetFloat("run", Input.GetKey(KeyCode.LeftControl) ? axisV : 0);
            }
            else
            {
                // Walking backward
                claireAnimator.SetBool("walkBack", true);
                claireAnimator.SetBool("walk", false);
            }
        }
        else
        {
            claireAnimator.SetBool("walk", false);
            claireAnimator.SetBool("walkBack", false);
            claireAnimator.SetFloat("run", 0);
        }

        // Rotation - Only rotate when there's a horizontal input
        if (Mathf.Abs(axisH) > 0.1f)
        {
            Quaternion targetRotation = Quaternion.Euler(0, axisH * rotSpeed * Time.deltaTime + transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }

        // Idle Dance logic
        if (axisH == 0 && axisV == 0)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                claireAnimator.SetBool("dance", true);
                transform.Find("AudioDance").GetComponent<AudioSource>().enabled = true;
            }
        }
        else
        {
            countdown = timeout;
            claireAnimator.SetBool("dance", false);
            transform.Find("AudioDance").GetComponent<AudioSource>().enabled = false;
        }

        // Debug Dead 
        if (Input.GetKeyDown(KeyCode.AltGr))
        {
            ClaireDead();
        }

        // Jump height adjustment curve
        if (isJumping)
        {
            claireCapsule.height = claireAnimator.GetFloat("colheight");
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce);
            claireAnimator.SetTrigger("jump");
            claireAudioSource.pitch = 1f;
            claireAudioSource.PlayOneShot(sndJump);
        }
    }

    public void ClaireDead()
    {
        claireAnimator.SetTrigger("dead");
        GetComponent<ClaireController>().enabled = false;
    }

    public void PlaySoundImpact()
    {
        claireAudioSource.pitch = 1f;
        claireAudioSource.PlayOneShot(sndImpact);
    }

    public void PlayFootStep()
    {
        if (!claireAudioSource.isPlaying)
        {
            switchFoot = !switchFoot;
            claireAudioSource.pitch = 2f;
            claireAudioSource.PlayOneShot(switchFoot ? sndLeftFoot : sndRightFoot);
        }
    }

    public void SwitchIsJumping()
    {
        isJumping = false;
    }
}
