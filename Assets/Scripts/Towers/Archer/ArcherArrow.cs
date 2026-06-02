using UnityEngine;
using UnityEngine.UIElements;

public class ArcherArrow : Projectile
{
    protected override void DealDamage()
    {
        if (_targetTransform != null && _targetTransform.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage(_damage);
        }

        Debug.Log("DEAL WITH IT!");
        Destroy(gameObject);
    }
}
