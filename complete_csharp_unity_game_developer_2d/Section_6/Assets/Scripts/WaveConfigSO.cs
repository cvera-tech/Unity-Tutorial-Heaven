using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Path Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private Transform _pathPrefab;
    [SerializeField] private float _moveSpeed = 5f;

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
}
