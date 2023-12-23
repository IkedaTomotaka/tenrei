using System.Collections.Generic;
using UnityEngine;

public class GachaResultManager : MonoBehaviour
{
    // シングルトンのインスタンス
    public static GachaResultManager Instance;
    //public Sprite currentBackground;

    // ガチャ結果のリスト
    public List<Sprite> gachaResults = new List<Sprite>();

    // シングルトンの設定
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ガチャ結果をクリアするメソッド
    public void ClearResults()
    {
        gachaResults.Clear();
    }
}