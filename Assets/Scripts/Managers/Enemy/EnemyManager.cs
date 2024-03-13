using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(EnemySpawnManager))]
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnManager spawnManager;
    [SerializeField] private ARPlaneManager planeManager;

    private ARPlane plane;
    private List<ARPlane> planes = new List<ARPlane>();
    private void Start()
    {
        planeManager.planesChanged += OnPlaneChanged;
    }
    private void OnDestroy()
    {
        planeManager.planesChanged -= OnPlaneChanged;
    }

    public Vector3 GetRandomPosition()
    {
        plane = GetLargestPlane();

        if(plane == null)
        {
            return Vector3.zero;
        }

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minZ = float.MaxValue;
        float maxZ = float.MinValue;

        foreach (Vector2 point in plane.boundary)
        {
            if (point.x < minX)
                minX = point.x;
            if (point.x > maxX)
                maxX = point.x;
            if (point.y < minZ)
                minZ = point.y;
            if (point.y > maxZ)
                maxZ = point.y;
        }

        float indentation = 0.2f;
        minX += indentation;
        minZ += indentation;
        maxX -= indentation;
        maxZ -= indentation;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, 0, randomZ);
    }

    private void OnPlaneChanged(ARPlanesChangedEventArgs eventArgs)
    {
        for (int i = 0; i < eventArgs.updated.Count; i++)
        {
            if (eventArgs.updated[i].alignment == PlaneAlignment.HorizontalUp || eventArgs.updated[i].alignment == PlaneAlignment.HorizontalDown)
            {
                planes.Add(eventArgs.updated[i]);
            }
        }
    }

    private ARPlane GetLargestPlane()
    {
        ARPlane plane = null;

        float maxSquare = 0;

        for(int i = 0; i < planes.Count; i++)
        {
            float currentSquare = planes[i].size.x * planes[i].size.y;

            if (currentSquare > maxSquare)
            {
                maxSquare = currentSquare;
                plane = planes[i];
            }
        }

        return plane;
    }

    #region Validation
    private void OnValidate()
    {
        if (spawnManager == null)
        {
            if (TryGetComponent(out EnemySpawnManager spawnManager))
            {
                this.spawnManager = spawnManager;
            }
        }
    }
    #endregion
}
