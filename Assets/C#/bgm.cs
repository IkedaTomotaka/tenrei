using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
    private static bgm instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public static bgm Instance
    {
        get{return instance;}
    }

    public void PlayBGM(AudioClip bgm)
    {
        audioSource.clip = bgm;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}