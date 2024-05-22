using UnityEngine;
using UnityEngine.UI;

public class ItemCountDisplay : MonoBehaviour
{
    public Text itemCountText;

    void Start()
    {
        UpdateItemCountText();
    }

    void Update()
    {
        UpdateItemCountText();
    }

    public void UpdateItemCountText()
    {
        itemCountText.text = string.Format("{0}", GameManager.Instance.gachaItemCount);
    }
}
