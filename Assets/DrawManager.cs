using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] int _drawAmount;
    [SerializeField] TextMeshProUGUI drawText;
    [SerializeField] HandOfCards hand;

    public int DrawAmount => _drawAmount;


    private void Update()
    {
        SetDrawAmount();
    }

    public void SetDrawAmount()
    {
        if(_drawAmount <= 0) _drawAmount = Mathf.Clamp(_drawAmount, 1, hand.MaxHandSize);
        drawText.text = "Draw" + (DrawAmount > 1 ? " (" + DrawAmount.ToString() + ")" : "");
        
    }

    public void DrawCards()
    {
        hand.DrawNCards(_drawAmount);
    }
}
