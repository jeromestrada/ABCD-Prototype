using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private float _fadeInSpeed = 0.08f;
    [SerializeField] private float _fadeOutSpeed = 0.08f;
    public bool isDisplayed;
    private Image _uiImage;

    private void Start()
    {
        _mainCamera = Camera.main;
        _uiImage = _uiPanel.GetComponentInChildren<Image>();
        _uiPanel.SetActive(false);
        isDisplayed = false;
    }

    private void LateUpdate()
    {
        var rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        if (_uiImage.color.a == 0)
        {
            _uiPanel.SetActive(false); // if the image is completely faded out, disable the panel
            isDisplayed = false;
        }
    }

    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        _uiPanel.SetActive(true);
        _uiImage.CrossFadeAlpha(1f, _fadeInSpeed, false);
        _promptText.CrossFadeAlpha(1f, _fadeInSpeed, false);
        isDisplayed = true;
    }

    public void Close()
    {
        _uiImage.CrossFadeAlpha(0f, _fadeOutSpeed, false);
        _promptText.CrossFadeAlpha(0f, _fadeOutSpeed, false);
    }


}
