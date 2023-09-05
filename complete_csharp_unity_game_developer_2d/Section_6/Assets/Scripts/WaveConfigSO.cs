using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Path Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private Transform _pathPrefab;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _delayBetweenEnemySpawns = 1f;
    [SerializeField] private float _spawnDelayVariance = 0f;
    [SerializeField] private float _minimumSpawnDelay = 0.5f;

    public float MoveSpeed => _moveSpeed;

    public int EnemyCount => _enemyPrefabs.Count;

    public List<Transform> Waypoints
    {
        get
        {
            List<Transform> waypoints = new();
            foreach(Transform child in _pathPrefab)
            {
                waypoints.Add(child);
            }
            return waypoints;
        }
    }
    
    public GameObject GetEnemyAtIndex(int index)
    {
        return _enemyPrefabs[index];
    }

    public float GetRandomSpawnDelay()
    {
        float randomDelay = Random.Range(_delayBetweenEnemySpawns - _spawnDelayVariance, _delayBetweenEnemySpawns + _spawnDelayVariance);
        return Mathf.Max(_minimumSpawnDelay, randomDelay);
    }
}
