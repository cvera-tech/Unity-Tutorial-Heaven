using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> _waveConfigs;
    [SerializeField] private float _delayBetweenWaves = 1f;
    [SerializeField] private bool _isLooping = false;

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
        do
        {
            for (; _currentWaveIndex < _waveConfigs.Count; _currentWaveIndex = (_currentWaveIndex + 1) % _waveConfigs.Count)
            {
                for (int enemyIndex = 0; enemyIndex < CurrentWave.EnemyCount; enemyIndex++)
                {
                    GameObject currentEnemy = CurrentWave.GetEnemyAtIndex(enemyIndex);
                    Instantiate(currentEnemy, Waypoints[0].position, Quaternion.Euler(0, 0, 90), transform);
                    yield return new WaitForSeconds(CurrentWave.GetRandomSpawnDelay());
                }
                yield return new WaitForSeconds(_delayBetweenWaves);
            }
        }
        while(_isLooping);
    }
}
