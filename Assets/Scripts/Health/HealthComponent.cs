using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region Properties
    [SerializeField, Range(0, 100)]
    private float _startHealth;

    [SerializeField] private GameObject onTakeDamageEffect;
    [SerializeField] private GameObject onDeadEffect;
    #endregion

    public bool IsAlive => _currentHealth > 0;

    /// <summary>
    /// 1 - Start health / 2 - Current health
    /// </summary>
    public event Action<float, float> onHealthChange;
    public event Action onDead;


    private float _currentHealth;
    private void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(float damage)
    {
        if(_currentHealth <= 0)
        {
            Debug.Log("Враг уже мертв");
            return;
        }

        _currentHealth = Math.Max(0, _currentHealth - damage);

        Instantiate(onTakeDamageEffect, transform.position, Quaternion.identity);
        onHealthChange?.Invoke(_startHealth, _currentHealth);

        if(_currentHealth <= 0)
        {
            Instantiate(onDeadEffect, transform.position, Quaternion.identity);
            onDead?.Invoke();
        }
    }
}
