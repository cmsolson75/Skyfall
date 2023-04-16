using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMannager : MonoBehaviour
{
    public static AudioMannager Instance;
    private AudioSource Source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        Source.PlayOneShot(clip, volume);
    }
}
