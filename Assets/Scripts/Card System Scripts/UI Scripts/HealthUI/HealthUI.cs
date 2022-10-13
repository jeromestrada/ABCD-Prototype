using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Transform _ui;
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Transform _healthUITarget;
    [SerializeField] private Transform _cam;
    [SerializeField] private Canvas _canvas;
    private Image _healthBarSlider;

    private void Start()
    {
        _ui = Instantiate(_healthBarPrefab, _canvas.transform).transform;
        _healthBarSlider = _ui.GetChild(0).GetComponent<Image>();
        _ui.gameObject.SetActive(true);

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        if(_ui != null)
        {
            _ui.gameObject.SetActive(true);
            float healthPercent = currentHealth / (float)maxHealth;
            _healthBarSlider.fillAmount = healthPercent;

            if(currentHealth <= 0)
            {
                Destroy(_ui.gameObject);
            }
        }
    }

    private void LateUpdate()
    {
        if(_ui != null)
        {
            _ui.position = _healthUITarget.position;
            _ui.forward = -_cam.forward;
        }
    }
}
