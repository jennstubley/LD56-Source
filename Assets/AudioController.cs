using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip doorClip;
    public AudioClip safeClip;
    public AudioClip jumpClip;
    public AudioClip hitClip;
    public AudioClip plateClip;
    public AudioClip pickupClip;
    public AudioClip dropClip;
    public AudioClip finalDoorClip;
    public AudioClip shrinkClip;
    public AudioClip unshrinkClip;


    private AudioSource source;

    public static AudioController Instance;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDoor()
    {
        source.clip = doorClip;
        source.Play();
    }

    public void PlaySafe()
    {
        source.clip = safeClip;
        source.Play();
    }

    public void PlayJump()
    {
        source.clip = jumpClip;
        source.Play();
    }

    public void PlayHit()
    {

        source.clip = hitClip;
        source.Play();
    }

    public void PlayPlate()
    {
        source.clip = plateClip;
        source.Play();
    }

    public void PlayPickUp()
    {
        source.clip = pickupClip;
        source.Play();
    }

    public void PlayDrop()
    {
        source.clip = dropClip;
        source.Play();
    }

    public void PlayFinalDoor()
    {
        source.clip = finalDoorClip;
        source.Play();
    }

    public void PlayShrink()
    {
        source.clip = shrinkClip;
        source.Play();
    }

    public void PlayUnshrink()
    {
        source.clip = unshrinkClip;
        source.Play();
    }
}
