using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections.Generic;

public class GachaController : MonoBehaviour
{
    public static GachaController Instance; // シングルトンのインスタンス

    public enum GachaType
    {
        Type1,
        Type2,
        Type3
    }

    private GachaType currentGacha;
    //public Image gachaImage; // ガチャ画像
    public Image BackImage;
    public VideoPlayer BackVideo;
    public float delayTime = 1f;
    //public Sprite[] gachaTypeSprites;//ガチャタイプ毎のスプライト配列
    public Sprite[] gachaTypeBack;

    public Sprite[] allSprites; // すべてのスプライト
    public VideoClip[] gachaTypeVideo;

    private void Awake()
    {
        // シングルトンのインスタンスを設定
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentGacha = GachaType.Type1; // 初期ガチャタイプを設定
        UpdateGachaImage();//初期
    }


    public void OnRightButtonPressed()
    {
        Invoke("Right", delayTime);
    }

    void Right()
    {        
        // 右ボタンを押したときのガチャタイプの変更
        switch (currentGacha)
        {
            case GachaType.Type1:
                currentGacha = GachaType.Type2;
                break;
            case GachaType.Type2:
                currentGacha = GachaType.Type3;
                break;
            case GachaType.Type3:
                currentGacha = GachaType.Type1;
                break;
        }
        UpdateGachaImage(); // ガチャ画像の更新
    }

    public void OnLeftButtonPressed()
    {
        Invoke("Left", delayTime);
    }

    void Left()
    {
        // 左ボタンを押したときのガチャタイプの変更
        switch (currentGacha)
        {
            case GachaType.Type1:
                currentGacha = GachaType.Type3;
                break;
            case GachaType.Type2:
                currentGacha = GachaType.Type1;
                break;
            case GachaType.Type3:
                currentGacha = GachaType.Type2;
                break;
        }
        UpdateGachaImage(); // ガチャ画像の更新
    }

    public void OnSingleGachaButtonPressed()
    {
        GachaResultManager.Instance.gachaResults.Add(UpdateGacha());
        GachaPurchaseConfirmation.Instance.OnSingleGachaButtonPressed();
    }

    public void OnTenTimesGachaButtonPressed()
    {
        for (int i = 0; i < 10; i++)
        {
            GachaResultManager.Instance.gachaResults.Add(UpdateGacha());
        }
        GachaPurchaseConfirmation.Instance.OnTenTimesGachaButtonPressed();
    }

    // ガチャの画像を更新する
    private void UpdateGachaImage()
    {
        //gachaImage.sprite = gachaTypeSprites[(int)currentGacha];
        BackImage.sprite = gachaTypeBack[(int)currentGacha];
        // 既存の動画があれば停止する
        if (BackVideo.isPlaying) 
        { 
            BackVideo.Stop();
        } 
        // 新しい動画を設定 
        BackVideo.clip = gachaTypeVideo[(int)currentGacha];
        // 動画の再生を開始 
        BackVideo.Play();
    }

    // ガチャの背景画像を取得する
    public Sprite GetBackImage()
    {
        return gachaTypeBack[(int)currentGacha];
    }

    // ガチャを更新して結果のスプライトを返す
    public Sprite UpdateGacha()
    {
        int randomIndex;

        switch (currentGacha)
        {
            case GachaType.Type1:
                randomIndex = Random.Range(0, 17);
            break;
            case GachaType.Type2:
                if (Random.value < 0.8f)
                    randomIndex = Random.Range(0, 15);
                else
                    randomIndex = Random.Range(17, 18);
                break;
            case GachaType.Type3:
                if (Random.value < 0.8f)
                    randomIndex = Random.Range(0, 14);
                else
                    randomIndex = Random.Range(16, 18);
                break;
            default:
                randomIndex = 0;
                break;
        }

        Sprite selectedSprite = allSprites[randomIndex]; // 選択したスプライト
        return selectedSprite; // 選択したスプライトを返す
    }

    // 現在のガチャタイプを保存する
    private void SaveCurrentGacha()
    {
        PlayerPrefs.SetInt("CurrentGacha", (int)currentGacha);
    }
}