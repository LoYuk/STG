using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw3DBezierCurve : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints; // Control points for the Bézier curve
    private Vector3 gizmosPosition; // Position for drawing gizmos
    public GameObject platformPrefab; // Prefab for the platform

    public float interval = 0.1f;

    private void Start()
    {
        InstantiatePrefabsAlongCurve();
    }


    private void OnDrawGizmos()
    {
        // Ensure there are enough control points
        if (controlPoints.Length < 4)
        {
            Debug.LogWarning("At least 4 control points are required to draw a cubic Bézier curve.");
            return;
        }

        // Draw the Bézier curve
        for (int i = 0; i < controlPoints.Length - 3; i += 3)
        {
            for (float t = 0; t <= 1; t += interval)
            {
                gizmosPosition = 
                    Mathf.Pow(1 - t, 3) * controlPoints[i].position + 
                    3 * Mathf.Pow(1 - t, 2) * t * controlPoints[i + 1].position + 
                    3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[i + 2].position + 
                    Mathf.Pow(t, 3) * controlPoints[i + 3].position;         

                Gizmos.DrawSphere(gizmosPosition, 0.25f);
            }
        }

        // Draw lines between control points
        for (int i = 0; i < controlPoints.Length; i += 2)
        {
            Gizmos.DrawLine(controlPoints[i].position, controlPoints[i + 1].position + Vector3.up * 0.1f); // Draw a line in 3D
        }
    }

    private void InstantiatePrefabsAlongCurve()
    {
        // Ensure there are enough control points
        if (controlPoints.Length < 4)
        {
            Debug.LogWarning("At least 4 control points are required to instantiate prefabs along the curve.");
            return;
        }

        // Instantiate prefabs along the Bézier curve
        for (int i = 0; i < controlPoints.Length - 3; i += 3)
        {
            for (float t = 0; t <= 1; t += interval)
            {
                Vector3 position = 
                    Mathf.Pow(1 - t, 3) * controlPoints[i].position + 
                    3 * Mathf.Pow(1 - t, 2) * t * controlPoints[i + 1].position + 
                    3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[i + 2].position + 
                    Mathf.Pow(t, 3) * controlPoints[i + 3].position;

                Instantiate(platformPrefab, position, Quaternion.identity);
            }
        }
    }
}