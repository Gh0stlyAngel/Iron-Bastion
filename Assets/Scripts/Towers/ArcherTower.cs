using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[SelectionBase]
public class ArcherTower : Tower
{
    [SerializeField] private ArcherArrow _arrow;
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private float _arrowDamage;

    protected override void Attack(Transform target)
    {
        ArcherArrow newarrow = Instantiate(_arrow, _shootingPoint.transform.position, transform.rotation);
        newarrow.Shot(_arrowDamage, _arrowSpeed, _currentEnemy.transform);
    }
}
