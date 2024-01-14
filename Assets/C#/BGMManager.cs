using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] bgmClips;
    public float delayInSeconds = 3.0f; // BGM再生の遅延時間（インスペクターで変更可能）
    public float fadeTime = 3.0f;

    public static BGMManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.buildIndex);
    }

    private void PlayBGM(int sceneIndex)
    {
        // インデックスが0の場合は何もしない
        if (sceneIndex == 0) return;

        // インデックスが1または2の場合、すでに再生中でなければBGMを再生（遅延なし）
        if ((sceneIndex == 1 || sceneIndex == 2) && bgmClips.Length > 1 && bgmClips[1] != null)
        {
            
            if(sceneIndex == 1)
            {
                StartCoroutine(PlayDelayedBGM(bgmClips[1], delayInSeconds));
            }
            if(sceneIndex == 2)
            {
                PlayBGMClip(bgmClips[1]);
                StartCoroutine(FadeOutCoroutine(fadeTime));
            }
        }
        // インデックスが3または4の場合、同様に処理
        else if ((sceneIndex == 3 || sceneIndex == 4) && bgmClips.Length > 2 && bgmClips[2] != null)
        {
            
            if(sceneIndex == 3)
            {
                StartCoroutine(PlayDelayedBGM(bgmClips[2], delayInSeconds));
            }
            if(sceneIndex == 4)
            {
                PlayBGMClip(bgmClips[2]);
            }
        }
        // インデックスが5または6の場合、同様に処理
        else if ((sceneIndex == 5 || sceneIndex == 6) && bgmClips.Length > 3 && bgmClips[3] != null)
        {
            
            if(sceneIndex == 5)
            {
                StartCoroutine(PlayDelayedBGM(bgmClips[3], delayInSeconds));
            }
            if(sceneIndex == 6)
            {
                PlayBGMClip(bgmClips[3]);
            }
        }
        /*// インデックスが0の場合は何もしない
        if (sceneIndex == 0) return;

        // インデックスが1の場合、指定された遅延時間後にBGMを再生
        if (sceneIndex == 1 && bgmClips.Length > 4 && bgmClips[1] != null)
        {
            StartCoroutine(PlayDelayedBGM(bgmClips[1], delayInSeconds));
        }
        // インデックスが2の場合、BGMをフェードアウト
        if (sceneIndex == 2 && bgmClips.Length > 4 && bgmClips[1] != null)
        {
            //StartCoroutine(PlayDelayedBGM(bgmClips[2], delayInSeconds));
            StartCoroutine(FadeOutCoroutine(audioSource, fadeTime));
        }
        // インデックスが1の場合、指定された遅延時間後にBGMを再生
        if (sceneIndex == 3 && bgmClips.Length > 4 && bgmClips[2] != null)
        {
            StartCoroutine(PlayDelayedBGM(bgmClips[2], delayInSeconds));
        }
        // インデックスが1の場合、指定された遅延時間後にBGMを再生
        if (sceneIndex == 4 && bgmClips.Length > 4 && bgmClips[2] != null)
        {
            StartCoroutine(PlayDelayedBGM(bgmClips[2], delayInSeconds));
        }
        // インデックスが1の場合、指定された遅延時間後にBGMを再生
        if (sceneIndex == 5 && bgmClips.Length > 4 && bgmClips[3] != null)
        {
            StartCoroutine(PlayDelayedBGM(bgmClips[3], delayInSeconds));
        }
        // インデックスが1の場合、指定された遅延時間後にBGMを再生
        if (sceneIndex == 6 && bgmClips.Length > 4 && bgmClips[3] != null)
        {
            StartCoroutine(PlayDelayedBGM(bgmClips[3], delayInSeconds));
        }*/

        Debug.Log(bgmClips);
    }

    private void PlayBGMClip(AudioClip clip)
    {
        // 既に同じBGMが再生されている場合は何もしない
        if (audioSource.clip == clip && audioSource.isPlaying) return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    private IEnumerator FadeOutCoroutine(float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // ボリュームを元に戻す
    }

    IEnumerator PlayDelayedBGM(AudioClip clip, float delayInSeconds)
    {
        audioSource.clip = clip;
        yield return new WaitForSeconds(delayInSeconds);
        audioSource.Play();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
