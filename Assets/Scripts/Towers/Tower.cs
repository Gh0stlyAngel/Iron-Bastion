using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using NUnit.Framework.Internal;

[SelectionBase]
public abstract class Tower : MonoBehaviour
{
    public int Cost => _cost;
    [SerializeField] private int _cost;

    private List<EnemyMovement> _enemies = new();

    [SerializeField] protected EnemyMovement _currentEnemyMovement;

    [SerializeField] private float _shootDelay;
    [SerializeField] protected GameObject _shootingPoint;

    [SerializeField] private SoundType _soundType;

    protected virtual void Attack(Transform target)
    {
        AudioManager.Instance.PlaySFX(_soundType);
    }

    public virtual int SellPrice => Cost / 2;

    protected virtual void Start()
    {
        StartCoroutine(Shooter());
    }

    private void Update()
    {
        if (_currentEnemyMovement != null)
        {
            Debug.DrawLine(transform.position, _currentEnemyMovement.transform.position, color: Color.red);
            return;
        }
        else if (_enemies.Count > 0)
        {
            _enemies.RemoveAll(enemy => enemy == null);
            if (_enemies.Count > 0)
            {
                _currentEnemyMovement = SelectTarget();

                if (_currentEnemyMovement != null)
                {
                    _currentEnemyMovement.Enemy.Died += OnCurrentEnemyDied;
                }
                
            }
                

            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyMovement>(out var enemyMovement))
        {
            if (CanAttack(enemyMovement) && !_enemies.Contains(enemyMovement))
            {
                _enemies.Add(enemyMovement);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<EnemyMovement>(out var enemyMovement))
        {
            if (_enemies.Contains(enemyMovement))
            {
                _enemies.Remove(enemyMovement);
            }
            if (_currentEnemyMovement == enemyMovement && _currentEnemyMovement != null)
            {
                _currentEnemyMovement.Enemy.Died -= OnCurrentEnemyDied;
                _currentEnemyMovement = null;
            }
        }
    }

    private IEnumerator Shooter()
    {
        while (true)
        {
            if (_currentEnemyMovement == null)
            {
                yield return null;
                continue;
            }

            Attack(_currentEnemyMovement.TagetPoint);

            yield return new WaitForSeconds(_shootDelay);
        }
    }

    protected virtual EnemyMovement SelectTarget()
    {
        if (_enemies.Count == 0)
            return null;

        EnemyMovement best = null;
        int bestCurrentWaypoint = int.MinValue;

        for (int i = 0; i < _enemies.Count; i++)
        {
            Enemy enemy = _enemies[i].Enemy;

            if (enemy.IsDead)
                continue;

            if (bestCurrentWaypoint < _enemies[i].CurrentWaypoint)
            {
                best = _enemies[i];
                bestCurrentWaypoint = _enemies[i].CurrentWaypoint;
            }
        }

        return best;
    }

    protected virtual bool CanAttack(EnemyMovement enemy)
    {
        return true;
    }

    private void OnCurrentEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnCurrentEnemyDied;
        if (_currentEnemyMovement != null && enemy == _currentEnemyMovement.Enemy)
        {
            _enemies.Remove(_currentEnemyMovement);
            _currentEnemyMovement = null;
        }
    }
}
