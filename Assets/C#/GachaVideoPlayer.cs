using UnityEngine;
using System.Collections;
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
    private int currentResultIndex = 0; // 現在のガチャ結果のインデックス


    void Start()
    {
        gachaResults = GachaResultManager.Instance.gachaResults;
        videoPlayer.loopPointReached += OnVideoFinished;
        PlayInitialVideo();
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
        // 13秒後にガチャ結果の動画を再生
        Invoke("PlayGachaResultVideo", 5f);
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

                // 再生時間を設定
                float delay = (currentResult.name == "17" || currentResult.name == "18") ? 10f : 5f;
                StartCoroutine(WaitAndPlayNextVideo(delay));
                Debug.Log(delay);
                Debug.Log(currentResultIndex);
            }

            currentResultIndex++; // 次の結果のインデックスに進む
        }
    }

    IEnumerator WaitAndPlayNextVideo(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayGachaResultVideo();
    }
    
    bool IsSpecialConditionMet(List<Sprite> results)
    {
        // 特別な条件をチェック
        // ここでは、Spriteの名前または別の識別子を使用して17または18であるかを確認します
        bool containsSpecialSprite = results.Any(sprite => sprite.name == "17" || sprite.name == "18");

        // 17または18のSpriteが含まれていて、かつ10%の確率でtrueを返す
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
