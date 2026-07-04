using UnityEngine;

public class MageTower : Tower
{
    [SerializeField] private MagicMissile _magicMissile;
    [SerializeField] private float _missileSpeed;
    [SerializeField] private float _missileDamage;

    protected override void Attack(Transform target)
    {
        base.Attack(target);
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        MagicMissile newMissile = Instantiate(_magicMissile, _shootingPoint.transform.position, rotation);
        newMissile.Shot(_missileDamage, _missileSpeed, target);
    }
}
