using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] bgmClips;
    //public float delayInSeconds = 3.0f; // BGM再生の遅延時間（インスペクターで変更可能）
    //public float fadeTime = 3.0f;

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
    }private void PlayBGM(int sceneIndex)
{
    // インデックスが0の場合は何もしない
    if (sceneIndex == 0) return;

    // インデックスが5または6の場合、ボリュームを0.3に設定
    if (sceneIndex == 5 || sceneIndex == 6)
    {
        audioSource.volume = 0.3f;
    }
    else
    {
        audioSource.volume = 1f;
    }

    // インデックスが1または2の場合、BGMを再生
    if ((sceneIndex == 1 || sceneIndex == 2) && bgmClips.Length > 1 && bgmClips[1] != null)
    {
        PlayBGMClip(bgmClips[1], sceneIndex == 5);
    }
    // インデックスが3または4の場合、BGMを再生
    else if ((sceneIndex == 3 || sceneIndex == 4) && bgmClips.Length > 2 && bgmClips[2] != null)
    {
        PlayBGMClip(bgmClips[2], sceneIndex == 5);
    }
    // インデックスが5の場合、BGMを再生
    else if (sceneIndex == 5 && bgmClips.Length > 3 && bgmClips[3] != null)
    {
        PlayBGMClip(bgmClips[3], true); // インデックス5の場合は常に再生
    }
    // インデックスが6の場合、BGMを再生
    else if (sceneIndex == 6 && bgmClips.Length > 3 && bgmClips[3] != null)
    {
        PlayBGMClip(bgmClips[3], sceneIndex == 5);
    }
}

private void PlayBGMClip(AudioClip clip, bool forcePlay = false)
{
    // インデックスが5ではない場合、既に同じBGMが再生されているなら何もしない
    if (!forcePlay && audioSource.clip == clip && audioSource.isPlaying) return;

    audioSource.clip = clip;
    audioSource.Play();
}
}
