using System.Collections;
using UnityEngine;

public class FadeOutController : MonoBehaviour
{
   public float fadeTime = 2.0f;
   public float delayTime = 2.0f;

   private void Start()
   {
       StartCoroutine(DelayedFadeOutCoroutine(delayTime));
   }

   private IEnumerator DelayedFadeOutCoroutine(float delayTime)
   {
       yield return new WaitForSeconds(delayTime);

       if (bgm.Instance != null)
       {
           StartCoroutine(FadeOutCoroutine(bgm.Instance, fadeTime));
       }
       else
       {
           Debug.LogError("No bgm instance found.");
       }
   }

   private IEnumerator FadeOutCoroutine(bgm bgmInstance, float fadeTime)
   {
       AudioSource audioSource = bgmInstance.GetComponent<AudioSource>();
       while (audioSource.volume > 0.0f)
       {
           audioSource.volume -= Time.deltaTime / fadeTime;

           yield return null;
       }

       audioSource.volume = 0f;
       audioSource.Stop();
   }
}