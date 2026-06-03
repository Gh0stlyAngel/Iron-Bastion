using UnityEngine;

public class MageTower : Tower
{
    [SerializeField] private MagicMissile _magicMissile;
    [SerializeField] private float _missileSpeed;
    [SerializeField] private float _missileDamage;

    protected override void Attack(Transform target)
    {
        MagicMissile newMissile = Instantiate(_magicMissile, _shootingPoint.transform.position, transform.rotation);
        newMissile.Shot(_missileDamage, _missileSpeed, target);
    }
}
