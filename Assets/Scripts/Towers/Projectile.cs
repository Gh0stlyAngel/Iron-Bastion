using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Transform _targetTransform;
    protected float _damage;
    protected float _speed;
    protected bool _isShot;

    protected Vector3 _lastTargetPos;

    protected abstract void DealDamage();

    protected virtual void Update()
    {
        if (_isShot)
        {
            if(_targetTransform != null)
            {
                _lastTargetPos = _targetTransform.position;
            }

            Move();

            if ((_lastTargetPos - transform.position).magnitude <= 0.2f)
            {
                DealDamage();
            }
        }
    }

    protected virtual void Move()
    {
        transform.SetPositionAndRotation(Vector3.MoveTowards(
                transform.position, _lastTargetPos, _speed * Time.deltaTime),
                Quaternion.LookRotation(_lastTargetPos - transform.position));
    }

    public virtual void Shot(float damage, float speed, Transform target)
    {
        Debug.Log("Shot!");
        _damage = damage;
        _speed = speed;
        _targetTransform = target;
        _lastTargetPos = _targetTransform.position;
        _isShot = true;
    }
}
