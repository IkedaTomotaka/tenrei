using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GachaPurchaseConfirmation : MonoBehaviour
{
    public GameObject confirmationPanel; // 確認画面のパネル
    public GameObject Image;
    public Text confirmationText; // 確認テキスト
    public Button yesButton; // 確認ボタン
    public Button noButton; // キャンセルボタン
    public string nextSceneName;

    private int gachaItemCount; // ガチャアイテムの数
    public static GachaPurchaseConfirmation Instance; // シングルトンのインスタンス
    public Color textColor; // テキストの色
    public float delayTime = 0.1f; // 遅延時間（秒）
    void Awake()
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
        yesButton.onClick.AddListener(ConfirmPurchase);
        noButton.onClick.AddListener(CancelPurchase);

        confirmationPanel.SetActive(false);
        Image.SetActive(false);
    }

    // 10回ガチャを回すボタンが押された時の処理
    public void OnTenTimesGachaButtonPressed()
    {
        gachaItemCount = 300; // ガチャを回す回数に応じたアイテム消費数
        string colorCodeWithoutAlpha = ColorUtility.ToHtmlStringRGB(textColor);

        // 確認画面のテキストを設定し、画面を表示する
        confirmationText.text = string.Format("金剛賞を消費して、\nガチャを{0}回購入します。\n<color=#{3}>金剛賞 {1} → {2}</color> \nよろしいですか？", 10, GameManager.Instance.gachaItemCount, GameManager.Instance.gachaItemCount - gachaItemCount, colorCodeWithoutAlpha, textColor);
        confirmationPanel.SetActive(true);
        Image.SetActive(true);

        // アイテムがマイナスになる場合はボタンを非アクティブにする
        if (GameManager.Instance.gachaItemCount < gachaItemCount)
        {
            yesButton.interactable = false;
        }
        else
        {
            yesButton.interactable = true;
        }
    }

    // 1回ガチャを回すボタンが押された時の処理
    public void OnSingleGachaButtonPressed()
    {
        gachaItemCount = 30; // ガチャを回す回数に応じたアイテム消費数
        string colorCodeWithoutAlpha = ColorUtility.ToHtmlStringRGB(textColor);

        // 確認画面のテキストを設定し、画面を表示する
        confirmationText.text = string.Format("金剛賞を消費して、\nガチャを{0}回購入します。\n<color=#{3}>金剛賞 {1} → {2}</color> \nよろしいですか？", 1, GameManager.Instance.gachaItemCount, GameManager.Instance.gachaItemCount - gachaItemCount, colorCodeWithoutAlpha, textColor);
        confirmationPanel.SetActive(true);
        Image.SetActive(true);

        // アイテムがマイナスになる場合はボタンを非アクティブにする
        if (GameManager.Instance.gachaItemCount < gachaItemCount)
        {
            yesButton.interactable = false;
        }
        else
        {
            yesButton.interactable = true;
        }
    }

    // 購入の確認がされた時の処理
    private void ConfirmPurchase()
    {
        StartCoroutine(WithDelay());
    }

    IEnumerator WithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        GameManager.Instance.gachaItemCount -= gachaItemCount; // ガチャアイテムの数を更新
        Debug.Log("You now have " + GameManager.Instance.gachaItemCount + " gacha items.");
        confirmationPanel.SetActive(false); // 確認画面を非表示にする
        SceneManager.LoadScene(nextSceneName);
        Image.SetActive(false);
    }

    // 購入のキャンセルがされた時の処理
    private void CancelPurchase()
    {
        GachaResultManager.Instance.gachaResults.Clear();
        confirmationPanel.SetActive(false); // 確認画面を非表示にする
        Image.SetActive(false);
    }
}
