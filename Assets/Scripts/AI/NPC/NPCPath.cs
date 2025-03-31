using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class NPCPath : MonoBehaviour
{
    public List<Vector3> waypoints = new List<Vector3>();
    public Color pathColor = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = pathColor;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
            Gizmos.DrawSphere(waypoints[i], 0.2f);
        }
        if (waypoints.Count > 0)
        {
            Gizmos.DrawSphere(waypoints[waypoints.Count - 1], 0.2f);
        }
    }
}