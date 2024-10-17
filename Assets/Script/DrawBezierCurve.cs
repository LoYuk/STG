using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBezierCurve : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < (controlPoints.Length - 2); i += 2)
        {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[i].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[i + 1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[i + 3].position + Mathf.Pow(t, 3) * controlPoints[i + 2].position;         
                Gizmos.DrawSphere(gizmosPosition, 0.25f);
            }
        }

        for (int i = 0; i < controlPoints.Length; i += 2)
        {
            Gizmos.DrawLine(new Vector2(controlPoints[i].position.x, controlPoints[i].position.y), new Vector2(controlPoints[i + 1].position.x, controlPoints[i + 1].position.y));
        }
    }
} 

