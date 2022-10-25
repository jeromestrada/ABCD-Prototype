using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;

    public static System.Action<string, Vector2> OnMouseHover;
    public static System.Action OnMouseLoseFocus;

    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }
    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    void Start()
    {
        HideTip();
    }

    private void ShowTip(string tip, Vector2 mousePos)
    {
        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);
        float xOffset = tipWindow.sizeDelta.x;
        float yOffset = tipText.preferredHeight * 0.8f;
        if (mousePos.x + (tipWindow.sizeDelta.x * 2) >= Screen.width) xOffset *= -1; // handles tooltip edging over the screen
        if(mousePos.y + (tipText.preferredHeight * 2) >= Screen.height) yOffset *= -1;

        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(mousePos.x + xOffset, mousePos.y + yOffset);
    }

    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }

}
