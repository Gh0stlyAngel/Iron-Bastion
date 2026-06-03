using UnityEngine;

public class MagicMissile : Projectile
{
    [SerializeField] private DamageType _damageType;
    protected override void DealDamage()
    {
        Enemy enemy = _targetTransform.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(_damage, _damageType);
        }

        Debug.Log("DEAL WITH IT!");
        Destroy(gameObject);
    }
}
