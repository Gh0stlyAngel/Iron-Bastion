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

    protected EnemyMovement _currentEnemy;

    [SerializeField] private float _shootDelay;
    [SerializeField] protected GameObject _shootingPoint;

    protected abstract void Attack(Transform target);

    protected virtual void Start()
    {
        StartCoroutine(Shooter());
    }

    private void Update()
    {
        if (_currentEnemy != null)
        {
            Debug.DrawLine(transform.position, _currentEnemy.transform.position, color: Color.red);
            return;
        }
        else if (_enemies.Count > 0)
        {
            _enemies.RemoveAll(enemy => enemy == null);
            if (_enemies.Count > 0)
                _currentEnemy = SelectTarget();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyMovement>(out var enemyMovement))
        {
            if (CanAttack(enemyMovement) && !_enemies.Contains(enemyMovement))
            {
                _enemies.Add(enemyMovement);

                if (_currentEnemy == null)
                {
                    _currentEnemy = enemyMovement;
                }
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
            if (_currentEnemy == enemyMovement)
            {
                _currentEnemy = null;
            }
        }
    }

    private IEnumerator Shooter()
    {
        while (true)
        {
            if (_currentEnemy == null)
            {
                yield return null;
                continue;
            }

            Attack(_currentEnemy.TagetPoint);

            yield return new WaitForSeconds(_shootDelay);
        }
    }

    protected virtual EnemyMovement SelectTarget()
    {
        if (_enemies.Count == 0)
            return null;

        EnemyMovement best = _enemies[0];

        for (int i = 1; i < _enemies.Count; i++)
        {
            if(best.CurrentWaypoint < _enemies[i].CurrentWaypoint)
            {
                best = _enemies[i];
            }
        }

        return best;
    }

    protected virtual bool CanAttack(EnemyMovement enemy)
    {
        return true;
    }
}
