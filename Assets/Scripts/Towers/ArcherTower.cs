using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[SelectionBase]
public class ArcherTower : Tower
{
    private List<EnemyMovement> _enemies = new();

    private EnemyMovement _currentEnemy;

    [SerializeField] private ArcherArrow _arrow;
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private float _arrowDamage;

    [SerializeField] private float _shootDelay;
    [SerializeField] private GameObject _shootingPoint;

    private void Start()
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
            if (_enemies.Count > 0 )
                _currentEnemy = _enemies[0];  // В дальнейшем заменить на сравнение расстояния до следующего waypoint
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyMovement>(out var enemyMovement))
        {
            if (!_enemies.Contains(enemyMovement))
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

            ArcherArrow newarrow = Instantiate(_arrow, _shootingPoint.transform.position, transform.rotation);
            newarrow.Shot(_arrowDamage, _arrowSpeed, _currentEnemy.transform);

            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
