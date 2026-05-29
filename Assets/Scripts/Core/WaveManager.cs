using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _enemy;
    private Vector3 spawnPoint;
    [SerializeField] private Path _path;
    [SerializeField] private Base _base;

    private void Start()
    {
        GameObject newEnemy = Instantiate(_enemy, _spawnPoint.position, _enemy.transform.rotation);
        EnemyMovement enemy = newEnemy.GetComponent<EnemyMovement>();
        enemy.Initialize(_path);
        enemy.ReachedBase += _base.HandleEnemyReachedBase;
    }
}
