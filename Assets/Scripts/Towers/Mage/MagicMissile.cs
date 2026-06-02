using UnityEngine;

public class MagicMissile : Projectile
{
    [SerializeField] private DamageType _damageType;
    protected override void DealDamage()
    {
        if (_targetTransform != null && _targetTransform.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage(_damage, _damageType);
        }

        Debug.Log("DEAL WITH IT!");
        Destroy(gameObject);
    }
}
