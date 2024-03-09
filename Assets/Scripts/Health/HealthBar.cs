using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private HealthComponent _health;

    private void Start()
    {
        _health.onHealthChange += UpdateHealthBar;
        _health.onDead += OnDead;
    }
    private void Update()
    {
        _healthBar.transform.LookAt(Camera.main.transform);
    }
    private void OnDestroy()
    {
        _health.onHealthChange -= UpdateHealthBar;
        _health.onDead -= OnDead;
    }

    private void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthBar.color = Color.green;

        float onePercent = maxHealth / 100;
        float result = currentHealth / onePercent;

        _healthBar.fillAmount = result / 100;
    }
    private void OnDead()
    {
        _healthBar.color = Color.red;
        _healthBar.fillAmount = 1;
    }
}
