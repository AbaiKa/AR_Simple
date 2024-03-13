using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    [SerializeField] private EnemyManager manager;
    [SerializeField] private float movementSpeed;

    private Vector3 targetPosition;
    private void Start()
    {
        targetPosition = manager.GetRandomPosition();
    }
    private void Update()
    {
        if(transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }
        else
        {
            targetPosition = manager.GetRandomPosition();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, 0.1f);
    }
}
