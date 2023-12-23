using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GachaResultDisplay : MonoBehaviour
{
    public GameObject resultContainer1;  // 1つの結果を表示するためのコンテナ
    public GameObject resultContainer10; // 10個の結果を表示するためのコンテナ
    public GameObject resultPrefab;      // 結果表示のためのPrefab
    public Image backImage;              // 背景画像
    public float animationDelay = 1f; // アニメーションの遅延時間
    public float initialDelay = 0f; // 初回のアニメーション遅延時間

    void Start()
    {
        GachaResultManager resultManager = GachaResultManager.Instance;

        if (GachaResultManager.Instance.gachaResults.Count == 1)
        {
            resultContainer10.SetActive(false);
            InstantiateResultWithAnimation(resultPrefab, resultContainer1.transform, GachaResultManager.Instance.gachaResults[0]);
            resultPrefab.GetComponent<Image>().enabled = false; // Imageを非表示にする
        }

        else
        {
            resultContainer1.SetActive(false);
            StartCoroutine(PlayAnimations());
        }

        backImage.sprite = GachaController.Instance.GetBackImage();
    }

    IEnumerator PlayAnimations()
    {
        for (int i = 0; i < GachaResultManager.Instance.gachaResults.Count; i++)
        {
            float delay = i == 0 ? initialDelay : animationDelay; // 初回と2回目以降の遅延時間を切り替える

            yield return new WaitForSeconds(delay);
            InstantiateResultWithAnimation(resultPrefab, resultContainer10.transform, GachaResultManager.Instance.gachaResults[i]);
            resultPrefab.GetComponent<Image>().enabled = false;
        }
    }

    private void InstantiateResultWithAnimation(GameObject prefab, Transform parent, Sprite sprite)
    {
        GameObject result = Instantiate(prefab, parent);
        Image resultImage = result.GetComponent<Image>();
        resultImage.sprite = sprite;
        // アニメーションの開始
        resultImage.CrossFadeAlpha(0f, 0f, true);
        resultImage.enabled = true;
        resultImage.CrossFadeAlpha(1f, animationDelay, true);
    }

    public void OnRetryButtonPressed()
    {
        GachaResultManager.Instance.ClearResults();
        for (int i = 0; i < (resultContainer1.activeSelf ? 1 : 10); i++)
        {
            GachaResultManager.Instance.gachaResults.Add(GachaController.Instance.UpdateGacha());
        }
        if (resultContainer1.activeSelf)
        {
            GachaPurchaseConfirmation.Instance.OnSingleGachaButtonPressed();
        }
        else
        {
            GachaPurchaseConfirmation.Instance.OnTenTimesGachaButtonPressed();
        }
    }

    public void GatyaRetry()
    {
        GachaResultManager.Instance.gachaResults.Clear();
    }
}