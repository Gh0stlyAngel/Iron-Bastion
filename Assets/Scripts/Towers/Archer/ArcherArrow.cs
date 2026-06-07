using UnityEngine;
using UnityEngine.UIElements;

public class ArcherArrow : Projectile
{
    [SerializeField] private DamageType _damageType;
    protected override void DealDamage()
    {
        if (_targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }
        Enemy enemy = _targetTransform.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(_damage, _damageType);

            PlayOnHitEffect();
        }

        Debug.Log("DEAL WITH IT!");
        Destroy(gameObject);
    }
}
