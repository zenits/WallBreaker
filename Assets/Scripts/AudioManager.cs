using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip bombExplosionFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosionFX()
    {

    }


}
