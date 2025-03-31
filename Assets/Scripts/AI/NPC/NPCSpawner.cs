using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public List<GameObject> npcPrefabs;
    public Transform player;
    public float spawnRadius = 50f;
    public int maxNPCs = 10;
    private List<GameObject> activeNPCs = new List<GameObject>();
    public List<NPCPath> paths;

    void Update()
    {
        if (activeNPCs.Count < maxNPCs)
        {
            SpawnNPC();
        }
    }

    void SpawnNPC()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint();
        if (spawnPoint == Vector3.zero) return;

        GameObject npc = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Count)], spawnPoint, Quaternion.identity);
        NPCController controller = npc.GetComponent<NPCController>();
        controller.SetPath(paths[Random.Range(0, paths.Count)]);
        activeNPCs.Add(npc);
    }

    Vector3 GetRandomSpawnPoint()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPoint = player.position + (Random.insideUnitSphere * spawnRadius);
            randomPoint.y = 0;

            foreach (NPCPath path in paths)
            {
                foreach (Vector3 point in path.waypoints)
                {
                    if (Vector3.Distance(randomPoint, point) < 5f)
                    {
                        return point;
                    }
                }
            }
        }
        return Vector3.zero;
    }
}
