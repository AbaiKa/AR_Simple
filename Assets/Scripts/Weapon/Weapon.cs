using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;

    [Header("Properties")]
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDestroyTime;

    private void Start()
    {
        TouchManager.Instance.onClickToTarget += Shoot;
    }
    private void OnDestroy()
    {
        TouchManager.Instance.onClickToTarget -= Shoot;
    }
    private void Shoot(Vector3 target)
    {
        Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity).Init(_bulletSpeed, _damage, target, _bulletDestroyTime);
    }
}
