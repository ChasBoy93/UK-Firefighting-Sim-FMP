using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPCPath))]
public class NPCPathEditor : Editor
{
    private void OnSceneGUI()
    {
        NPCPath path = (NPCPath)target;
        Handles.color = Color.green;
        for (int i = 0; i < path.waypoints.Count; i++)
        {
            Vector3 newPoint = Handles.PositionHandle(path.waypoints[i], Quaternion.identity);
            if (newPoint != path.waypoints[i])
            {
                Undo.RecordObject(path, "Move Waypoint");
                path.waypoints[i] = newPoint;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        NPCPath path = (NPCPath)target;

        if (GUILayout.Button("Add Waypoint"))
        {
            Undo.RecordObject(path, "Add Waypoint");
            path.waypoints.Add(path.waypoints.Count > 0 ? path.waypoints[path.waypoints.Count - 1] + Vector3.forward : Vector3.zero);
        }

        if (GUILayout.Button("Clear Waypoints"))
        {
            Undo.RecordObject(path, "Clear Waypoints");
            path.waypoints.Clear();
        }
    }
}