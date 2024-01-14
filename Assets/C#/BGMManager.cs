using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource; // AudioSourceコンポーネントへの参照
    public AudioClip[] bgmClips; // 各シーンに対応するBGMの配列

    // シングルトンパターンの実装
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
        PlayBGM(scene.buildIndex); // シーンのビルドインデックスに基づいてBGMを再生
    }

    private void PlayBGM(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < bgmClips.Length && bgmClips[sceneIndex] != null)
        {
            audioSource.clip = bgmClips[sceneIndex];
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
