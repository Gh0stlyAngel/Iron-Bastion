using System.Runtime.CompilerServices;
using UnityEngine;

public class CannonShell : Projectile
{
    private float _explosionRadius;

    private Vector3 _startPosition;
    private Vector3 _targetPos;

    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    [SerializeField] private AnimationCurve _arcCurve;

    [SerializeField] private LayerMask _enemyMask;
    protected override void DealDamage()
    {

        Collider[] enemies =  Physics.OverlapSphere(transform.position, _explosionRadius, _enemyMask);

        foreach (var enemy in enemies)
        {
            if(enemy.gameObject.TryGetComponent<Enemy>(out var target))
            {
                target.TakeDamage(_damage);
            }
        }

        Debug.Log("DEAL WITH IT!");
        Destroy(gameObject);
    }

    protected override void Update()
    {
        if (_isShot)
        {
            Move();

            if ((_targetPos - transform.position).magnitude <= 0.1f)
            {
                DealDamage();
            }
        }
    }

    protected override void Move()
    {
        float totalDistance = (_targetPos - _startPosition).magnitude;
        float travelledDistance = (transform.position - _startPosition).magnitude;
        float progress = Mathf.Clamp01(travelledDistance / totalDistance);

        float arcHeight = totalDistance;
        arcHeight = Mathf.Clamp(arcHeight, minHeight, maxHeight);

        float height = _arcCurve.Evaluate(progress) * arcHeight;

        

        Vector3 targetPosition = _targetPos;
        targetPosition.y += height;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    public override void Shot(float damage, float speed, Transform target)
    {
        _startPosition = transform.position;
        
        base.Shot(damage, speed, target);
        _targetPos = target.position;
    }

    public void SetExplosionRadius(float radius)
    {
        _explosionRadius = radius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
