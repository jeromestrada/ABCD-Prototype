using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;
    private float xOffset;
    private float yOffset;
    private Vector2 mousePos;
    private bool isTipShown = false;

    

    private void OnEnable()
    {
        HoverTip.OnMouseHover += ShowTip;
        HoverTip.OnMouseLoseFocus += HideTip;
    }
    private void OnDisable()
    {
        HoverTip.OnMouseHover -= ShowTip;
        HoverTip.OnMouseLoseFocus -= HideTip;
    }

    void Start()
    {
        HideTip();
    }

    public void Update()
    {
        if (isTipShown)
        {
            mousePos = Input.mousePosition;
            UpdateTipPosition();
        }
    }

    private void ShowTip(string tip, Vector2 _mousePos)
    {
        isTipShown = true;
        mousePos = _mousePos;
        tipText.text = tip;
        float windowWidth = tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth;
        float windowHeight = tipText.preferredHeight > 100 ? 100 : tipText.preferredHeight;
        tipWindow.sizeDelta = new Vector2(windowWidth, windowHeight);
        xOffset = tipWindow.sizeDelta.x + 10;
        yOffset = tipWindow.sizeDelta.y * 0.8f - 10;
        if (mousePos.x + (tipWindow.sizeDelta.x * 2) >= Screen.width) xOffset *= -1; // handles tooltip edging over the screen
        if(mousePos.y + (tipWindow.sizeDelta.y * 2) >= Screen.height) yOffset *= -1;

        tipWindow.gameObject.SetActive(true);
        UpdateTipPosition();
    }

    private void UpdateTipPosition()
    {
        tipWindow.transform.position = new Vector2(mousePos.x + xOffset, mousePos.y + yOffset);
    }

    private void HideTip()
    {
        isTipShown = false;
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }

}
