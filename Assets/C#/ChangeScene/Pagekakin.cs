using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 

public class Pagekakin : MonoBehaviour
{
    public string NextStage;
    public float delayTime = 0.1f; // 遅延時間（秒）

 

    // ボタンが押された時の処理
    public void OnButtonPressed()
    {
        StartCoroutine(LoadNextSceneWithDelay());
    }

 

    IEnumerator LoadNextSceneWithDelay()
    {
        // 遅延時間待つ
        yield return new WaitForSeconds(delayTime);

 

        // シーンのロード
        SceneManager.LoadScene(NextStage);
    }
}