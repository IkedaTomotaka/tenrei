using System.Collections;
using UnityEngine;

public class BGMController : MonoBehaviour
{
   public AudioSource audioSource;
   public float fadeTime = 2.0f;

   private void Start()
   {
       StartCoroutine(FadeInCoroutine(audioSource, fadeTime));
   }

   private IEnumerator FadeInCoroutine(AudioSource audioSource, float fadeTime)
   {
       float startVolume = 0.0f;

       audioSource.volume = startVolume;
       audioSource.Play();

       while (audioSource.volume < 1.0f)
       {
           audioSource.volume += Time.deltaTime / fadeTime;

           yield return null;
       }

       audioSource.volume = 1f;
   }

   public void FadeOut()
   {
       StartCoroutine(FadeOutCoroutine(audioSource, fadeTime));
   }

   private IEnumerator FadeOutCoroutine(AudioSource audioSource, float fadeTime)
   {
       while (audioSource.volume > 0.0f)
       {
           audioSource.volume -= Time.deltaTime / fadeTime;

           yield return null;
       }

       audioSource.volume = 0f;
       audioSource.Stop();
   }
}