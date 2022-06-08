using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audioSource;
    public AudioClip Bgm;
    public AudioClip QteCorrectSound;
    public AudioClip QteWrongSound;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Bgm;
        audioSource.Play();


    }

    public void PlayKeyDownSound()
    {
        audioSource.PlayOneShot(QteCorrectSound, 2f);
    }

    public void WrongKeyDownSound()
    {
        audioSource.PlayOneShot(QteWrongSound, 7f);
    }
}
