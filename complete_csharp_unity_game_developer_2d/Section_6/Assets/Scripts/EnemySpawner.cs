using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> _waveConfigs;
    [SerializeField] private float _delayBetweenWaves = 1f;

    public List<Transform> Waypoints => CurrentWave.Waypoints;
    public float MoveSpeed => CurrentWave.MoveSpeed;

    private WaveConfigSO CurrentWave => _waveConfigs[_currentWaveIndex];

    private int _currentWaveIndex;

    private void Start()
    {
        _currentWaveIndex = 0;
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (; _currentWaveIndex < _waveConfigs.Count; _currentWaveIndex++)
        {
            for (int enemyIndex = 0; enemyIndex < CurrentWave.EnemyCount; enemyIndex++)
            {
                Instantiate(CurrentWave.GetEnemyAtIndex(0), transform);
                yield return new WaitForSeconds(CurrentWave.GetRandomSpawnDelay());
            }
            yield return new WaitForSeconds(_delayBetweenWaves);
        }
    }
}
