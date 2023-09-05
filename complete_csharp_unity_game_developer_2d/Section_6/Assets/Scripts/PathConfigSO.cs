using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Path Config")]
public class PathConfigSO : ScriptableObject
{
    [SerializeField] private Transform _pathPrefab;
    [SerializeField] private float _moveSpeed = 5f;

    public float MoveSpeed => _moveSpeed;

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
}
