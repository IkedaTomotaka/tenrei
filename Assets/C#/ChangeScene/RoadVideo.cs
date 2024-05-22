using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RoadVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoPlayer コンポーネントへの参照
    public VideoClip[] videoClips; // 動画クリップの配列

    void Start()
    {
        PlayRandomVideo();
    }

    void PlayRandomVideo()
    {
        // 配列からランダムに動画を選択
        int randomIndex = Random.Range(0, videoClips.Length);
        VideoClip selectedClip = videoClips[randomIndex];

        // 選択された動画を再生
        videoPlayer.clip = selectedClip;
        videoPlayer.Play();
    }
}
