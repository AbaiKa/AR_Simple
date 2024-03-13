using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemiesContainer;

    public void Spawn(Vector3 position)
    {
        GameObject e = Instantiate(_enemyPrefab, _enemiesContainer);
        e.transform.position = position;
    }
}
