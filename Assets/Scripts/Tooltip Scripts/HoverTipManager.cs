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
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);
        xOffset = tipWindow.sizeDelta.x;
        yOffset = tipText.preferredHeight * 0.8f;
        if (mousePos.x + (tipWindow.sizeDelta.x * 2) >= Screen.width) xOffset *= -1; // handles tooltip edging over the screen
        if(mousePos.y + (tipText.preferredHeight * 2) >= Screen.height) yOffset *= -1;

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
