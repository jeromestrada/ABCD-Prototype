using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(6,5)]
    public string tipToShow;
    private float timeToWait = 0.5f;

    public static System.Action<string, Vector2> OnMouseHover;
    public static System.Action OnMouseLoseFocus;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        OnMouseLoseFocus?.Invoke();
    }

    private void ShowMessage()
    {
        OnMouseHover?.Invoke(tipToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
