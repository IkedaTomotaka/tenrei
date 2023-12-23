using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
  public AudioClip sound1;
  AudioSource audioSource;

  void Start () 
  {
     //Componentを取得
     audioSource = GetComponent<AudioSource>();
  }

  public void OnButtonPressed()
  {
    audioSource.PlayOneShot(sound1);
  }
}