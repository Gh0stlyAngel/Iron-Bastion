using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Path _path;
    [SerializeField] private Base _base;

    [SerializeField] private List<Wave> _waves = new();

    [SerializeField] private float _spawnInterval;

    private void Start()
    {
        StartCoroutine(WaveSpawner());
        
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, _spawnPoint.position, enemyPrefab.transform.rotation);
        EnemyMovement enemy = newEnemy.GetComponent<EnemyMovement>();
        enemy.Initialize(_path);
        enemy.ReachedBase += _base.HandleEnemyReachedBase;
    }

    private IEnumerator WaveSpawner()
    {
        yield return new WaitForSeconds(1f);
        foreach (var wave in _waves)
        {
            foreach (var spawnData in wave.WaveData)
            {
                for (int i = 0; i < spawnData.EnemyCount; i++)
                {
                    SpawnEnemy(spawnData.EnemyPrefab);
                    yield return new WaitForSeconds(_spawnInterval);
                }
            }
            yield return new WaitForSeconds(wave.DelayAfterWave);
        }
    }
}
