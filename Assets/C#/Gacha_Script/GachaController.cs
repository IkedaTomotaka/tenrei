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
        Type3,
        Type4
    }

    private GachaType currentGacha;
    //public Image gachaImage; // ガチャ画像
    public Image BackImage;
    public Animator gachaAnimator;
    //public VideoPlayer BackVideo;
    public float delayTime = 1f;
    //public Sprite[] gachaTypeSprites;//ガチャタイプ毎のスプライト配列
    public Sprite[] gachaTypeBack;

    public Sprite[] allSprites; // すべてのスプライト
    //public VideoClip[] gachaTypeVideo;

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
                currentGacha = GachaType.Type4;
                break;
            case GachaType.Type4:
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
                currentGacha = GachaType.Type4;
                break;
            case GachaType.Type2:
                currentGacha = GachaType.Type1;
                break;
            case GachaType.Type3:
                currentGacha = GachaType.Type2;
                break;
            case GachaType.Type4:
                currentGacha = GachaType.Type3;
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
        switch (currentGacha)
        {
            case GachaType.Type1:
                gachaAnimator.Play("Suzaku");
                BackImage.sprite = gachaTypeBack[(int)currentGacha];
                break;
            case GachaType.Type2:
                gachaAnimator.Play("Genbu");
                BackImage.sprite = gachaTypeBack[(int)currentGacha];
                break;
            case GachaType.Type3:
                gachaAnimator.Play("Byakko");
                BackImage.sprite = gachaTypeBack[(int)currentGacha];
                break;
            case GachaType.Type4:
                gachaAnimator.Play("Seiryu");
                BackImage.sprite = gachaTypeBack[(int)currentGacha];
                break;
        }
        /*
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
        */
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
        float randomValue = Random.value;

        switch (currentGacha)
        {
            case GachaType.Type1:
                if (randomValue < 0.93f)
                    randomIndex = Random.Range(0, 16);
                else if(randomValue < 0.93f + 0.05f)
                    randomIndex = Random.Range(16, 17);
                else
                    randomIndex = Random.Range(17, 20);
            break;
            case GachaType.Type2:
                if (randomValue < 0.93f) // 99.4%の確率で0から15の間のインデックスを選択
                {
                    randomIndex = Random.Range(0, 16);
                }
                else if (randomValue < 0.93f + 0.05f) // 次の0.5%の確率で17のインデックスを選択
                {
                    randomIndex = Random.Range(17, 18); // この範囲は17のみを返します
                }
                else // 残りの確率で16、または18から19のインデックスを選択
                {
                    // このステップで、16または18/19のインデックスを等確率で選択するための追加のロジック
                    if (Random.value < 0.3f) // 50%の確率で16のインデックスを選択
                    {
                        randomIndex = 16;
                    }
                    else // 残りの50%の確率で18から19のインデックスを選択
                    {
                        randomIndex = Random.Range(18, 20); // この範囲は18または19を返します
                    }
                }
                break;
            case GachaType.Type3:
                if (randomValue < 0.93f)
                {
                    randomIndex = Random.Range(0, 16);
                }                    
                else if(randomValue < 0.93f + 0.05f)
                {
                    randomIndex = Random.Range(18, 19);
                }
            else // 残りの0.5%の確率で16、または18から19のインデックスを選択
            {
                // このステップで、16または18/19のインデックスを等確率で選択するための追加のロジック
                if (Random.value < 0.5f) // 50%の確率で16のインデックスを選択
                {
                    randomIndex = Random.Range(16, 18);
                }
                else // 残りの50%の確率で18から19のインデックスを選択
                {
                    randomIndex = Random.Range(19, 20);
                }
            }
                break;
            case GachaType.Type4:
                if (randomValue < 0.93f)
                    randomIndex = Random.Range(0, 16);
                else if(randomValue < 0.93f + 0.05f)
                    randomIndex = Random.Range(19, 20);
                    else
                    randomIndex = Random.Range(16, 19);
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