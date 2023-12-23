using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBGM : MonoBehaviour
{
    private void OnDisable()
    {
        bgm.Instance.StopBGM();
    }
}