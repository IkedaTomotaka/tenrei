using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections.Generic;
using System.Linq;

public class GachaVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip defaultVideo; // デフォルトの動画
    public VideoClip specialVideo; // 特別な条件で再生する動画
    public VideoClip[] gachaVideos; // ガチャ結果に対応する動画の配列
    private List<Sprite> gachaResults;
    public string nextSceneName; // 遷移先のシーン名
    private int currentResultIndex = 0; // 現在のガチャ結果のインデックス

    void Start()
    {
        gachaResults = GachaResultManager.Instance.gachaResults;
        videoPlayer.loopPointReached += OnVideoFinished;
        PlayInitialVideo();
    }

    void Update()
    {
        // マウスクリック（またはタップ）を検出
        if (Input.GetMouseButtonDown(0))
        {
            SkipToNextVideo();
        }
    }

    void SkipToNextVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        PlayGachaResultVideo();
    }

    void PlayInitialVideo()
    {
        if (IsSpecialConditionMet(gachaResults))
        {
            videoPlayer.clip = specialVideo;
        }
        else
        {
            videoPlayer.clip = defaultVideo;
        }
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        PlayGachaResultVideo();
    }

    void PlayGachaResultVideo()
    {
        if (currentResultIndex < gachaResults.Count)
        {
            Sprite currentResult = gachaResults[currentResultIndex];
            int resultIndex = GetGachaResultIndex(new List<Sprite> { currentResult });
            if (resultIndex >= 0 && resultIndex < gachaVideos.Length)
            {
                videoPlayer.clip = gachaVideos[resultIndex];
                videoPlayer.Play();
            }
            currentResultIndex++;
        }
        else
        {
            // 全ての動画が再生された後のシーン遷移
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void Please()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    
    bool IsSpecialConditionMet(List<Sprite> results)
    {
        // 特別な条件をチェック
        // ここでは、Spriteの名前または別の識別子を使用して17または18であるかを確認します
        bool containsSpecialSprite = results.Any(sprite => sprite.name == "17" || sprite.name == "18" || sprite.name == "19" || sprite.name == "20");

        // 対象のSpriteが含まれていて、かつ10%の確率でtrueを返す
        return containsSpecialSprite && Random.value < 0.1f;
    }

    int GetGachaResultIndex(List<Sprite> results)
    {
        if (results.Count > 0)
        {
            Sprite firstResult = results[0];
            string spriteName = firstResult.name;
            if (spriteName.StartsWith("Item_"))
            {
                string numberStr = spriteName.Substring("Item_".Length);
                if (int.TryParse(numberStr, out int number))
                {
                    return number - 1; // 配列のインデックスに合わせて調整
                }
            }
        }
        return -1;
    }
}
