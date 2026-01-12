using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [Header("Enemies to spawn")]
    [SerializeField] private List<GameObject> enemies;

    [Header("Spawn positions")]
    [SerializeField] private List<Transform> spawnPoints;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            SpawnEnemies();
            triggered = true;
        }
    }

    void SpawnEnemies()
    {
        int count = Mathf.Min(enemies.Count, spawnPoints.Count);

        for (int i = 0; i < count; i++)
        {
            Instantiate(enemies[i], spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}
