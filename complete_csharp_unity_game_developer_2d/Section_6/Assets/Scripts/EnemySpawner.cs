using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveConfigSO _waveConfig;

    public List<Transform> Waypoints => _waveConfig.Waypoints;
    public float MoveSpeed => _waveConfig.MoveSpeed;

    private void Start()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        Instantiate(_waveConfig.GetEnemyAtIndex(0), transform);
    }
}
