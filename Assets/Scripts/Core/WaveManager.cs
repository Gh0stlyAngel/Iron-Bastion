using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Path _path;
    [SerializeField] private Base _base;
    [SerializeField] private PlayerResources _playerResources;

    [SerializeField] private List<Wave> _waves = new();

    [SerializeField] private float _spawnInterval;

    private bool _allWavesSpawned;
    [SerializeField] private int _aliveEnemies;

    [SerializeField] private TextMeshProUGUI _waveText;

    [SerializeField] private bool _mainMenu;

    private void Start()
    {
        if (_waveText != null)
            _waveText.text = $"Wave 1 / {_waves.Count}";

        _allWavesSpawned = false;
        _aliveEnemies = 0;

        if (_mainMenu )
        {
            StartCoroutine(MainMenuWaveSpawner());
        }
    }

    public void StartWaves()
    {
        StartCoroutine(WaveSpawner());
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, _spawnPoint.position, enemyPrefab.transform.rotation);
        EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        Enemy enemy = newEnemy.GetComponent<Enemy>();

        enemyMovement.Initialize(_path);
        IncrementEnemyCounter();

        enemyMovement.ReachedBase += _base.HandleEnemyReachedBase;
        enemyMovement.ReachedBase += enemy.OnReachedBase;
        enemyMovement.ReachedBase += DecrementEnemyCounter;

        if (_playerResources != null)
            enemy.Died += _playerResources.OnEnemyDeath;
        enemy.Died += DecrementEnemyCounter;

    }

    private IEnumerator WaveSpawner()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < _waves.Count; i++)
        {
            Wave wave = _waves[i];
            if (_waveText != null)
                _waveText.text = $"Wave {i + 1} / {_waves.Count}";

            foreach (var spawnData in wave.WaveData)
            {
                for (int j = 0; j < spawnData.EnemyCount; j++)
                {
                    SpawnEnemy(spawnData.EnemyPrefab);
                    yield return new WaitForSeconds(_spawnInterval);
                }
            }

            if (i < _waves.Count - 1)
            {
                yield return new WaitForSeconds(wave.DelayAfterWave);
            }
        }

        _allWavesSpawned = true;
        CheckVictory();
    }

    private IEnumerator MainMenuWaveSpawner()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Wave wave = _waves[0];
            foreach (var spawnData in wave.WaveData)
            {
                for (int j = 0; j < spawnData.EnemyCount; j++)
                {
                    SpawnEnemy(spawnData.EnemyPrefab);
                    yield return new WaitForSeconds(_spawnInterval);
                }
            }
            yield return new WaitForSeconds(wave.DelayAfterWave);
        }
    }

    private void IncrementEnemyCounter()
    {
        _aliveEnemies++;
    }

    private void DecrementEnemyCounter(Enemy _)
    {
        _aliveEnemies--;

        CheckVictory();
    }

    private void CheckVictory()
    {
        if (_allWavesSpawned && _aliveEnemies <= 0)
        {
            _gameManager.Victory();
        }
    }
}
