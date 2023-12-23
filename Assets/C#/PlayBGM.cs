using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    [SerializeField]private AudioClip bgmClip;
    // Start is called before the first frame update
    private void Start()
    {
        bgm.Instance.PlayBGM(bgmClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}