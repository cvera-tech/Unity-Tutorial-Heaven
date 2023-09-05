using System.Collections.Generic;
using UnityEngine;

public class PathNavigator : MonoBehaviour
{
    [SerializeField] private PathConfigSO _pathConfig;

    private List<Transform> _waypoints;
    private int _waypointIndex;

    private void Start()
    {
        _waypoints = _pathConfig.Waypoints;
        _waypointIndex = 0;
        transform.position = _waypoints[0].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            Vector2 targetPosition = _waypoints[_waypointIndex].position;
            float maxDelta = _pathConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, maxDelta);
            if ((Vector2)transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
