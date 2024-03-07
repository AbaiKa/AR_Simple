using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    private float _damage;
    private Vector3 _target;
    public void Init(float speed, float damage, Vector3 target, float destroyTime)
    {
        _speed = speed;
        _damage = damage;
        _target = target;

        Invoke(nameof(Crush), destroyTime);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out HealthComponent health))
        {
            health.TakeDamage(_damage);
        }

        Crush();
    }

    private void Crush()
    {
        Destroy(gameObject);
    }
}
