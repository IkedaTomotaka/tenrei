using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour
{
    public Button btn300;
    public Button btn3000;
    public Button btn10000;
    public GameObject confirmationPanel;
    public GameObject BackImage;
    public Text confirmationText;
    public Button yesButton;
    public Button noButton;
    public ItemCountDisplay itemCountDisplay;

    private int itemToPurchase;
    public Color textColor; // テキストの色

    void Start()
    {
        btn300.onClick.AddListener(() => StartPurchase(30));
        btn3000.onClick.AddListener(() => StartPurchase(300));
        btn10000.onClick.AddListener(() => StartPurchase(1000));
        yesButton.onClick.AddListener(ConfirmPurchase);
        noButton.onClick.AddListener(CancelPurchase);

        confirmationPanel.SetActive(false);
        BackImage.SetActive(false);
        itemCountDisplay.UpdateItemCountText();
    }

    void StartPurchase(int itemCount)
    {
        itemToPurchase = itemCount;
        string colorCodeWithoutAlpha = ColorUtility.ToHtmlStringRGB(textColor);
        // 確認画面のテキストを設定し、画面を表示する
        confirmationText.text = string.Format("金剛賞を購入します。\n<color=#{2}>金剛賞 {0} → {1}</color>\nよろしいですか？", GameManager.Instance.gachaItemCount, GameManager.Instance.gachaItemCount + itemCount, colorCodeWithoutAlpha, textColor);
        confirmationPanel.SetActive(true);
        BackImage.SetActive(true);
    }

    void ConfirmPurchase()
    {
        GameManager.Instance.gachaItemCount += itemToPurchase;
        Debug.Log("You now have " + GameManager.Instance.gachaItemCount + " gacha items.");
        itemCountDisplay.UpdateItemCountText();
        confirmationPanel.SetActive(false);
        BackImage.SetActive(false);
    }

    void CancelPurchase()
    {
        confirmationPanel.SetActive(false);
        BackImage.SetActive(false);
    }
}
