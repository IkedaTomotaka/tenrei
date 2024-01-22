using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectSpawner : MonoBehaviour
{
    public GameObject effectPrefab; // エディタから設定するエフェクトのプレハブ

    void Update()
    {
        // マウスがクリックされたか、またはタッチされたかを検出
        if (Input.GetMouseButtonDown(0))
        {
            SpawnEffectAtPosition(Input.mousePosition);
        }
    }

    void SpawnEffectAtPosition(Vector3 position)
    {
        // スクリーン座標をワールド座標に変換
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        worldPosition.z = 0; // エフェクトがカメラに隠れないようにZ座標を調整

        // エフェクトを生成し、1秒後に破棄
        GameObject effectInstance = Instantiate(effectPrefab, worldPosition, Quaternion.identity);
        Destroy(effectInstance, 1.0f); // 1秒後にエフェクトを削除
    }
}
