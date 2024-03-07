using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region Properties
    [SerializeField, Range(0, 100)]
    private float _startHealth;
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

        onHealthChange?.Invoke(_startHealth, _currentHealth);

        if(_currentHealth <= 0)
        {
            onDead?.Invoke();
        }
    }
}
