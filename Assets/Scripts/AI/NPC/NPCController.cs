using UnityEngine;

public class NPCController : MonoBehaviour
{
    private NPCPath path;
    private int waypointIndex = 0;
    public float speed = 2f;

    public void SetPath(NPCPath newPath)
    {
        path = newPath;
        waypointIndex = Random.Range(0, path.waypoints.Count);
    }

    void Update()
    {
        if (path == null || path.waypoints.Count == 0) return;

        Vector3 targetWaypoint = path.waypoints[waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint) < 0.5f)
        {
            waypointIndex = (waypointIndex + 1) % path.waypoints.Count;
        }
    }
}