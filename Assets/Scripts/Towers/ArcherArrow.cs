using UnityEngine;
using UnityEngine.UIElements;

public class ArcherArrow : MonoBehaviour
{
    private Transform _targetTransform;
    private float _damage;
    private float _speed;
    private bool _isShot;

    private Vector3 _lastTargetPos;

    private void Update()
    {
        if (_isShot)
        {
            if(_targetTransform != null)
            {
                _lastTargetPos = _targetTransform.position;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _lastTargetPos, _speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(_lastTargetPos - transform.position);

            if ((_lastTargetPos - transform.position).magnitude <= 0.2f)
            {
                if (_targetTransform != null && _targetTransform.gameObject.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.TakeDamage(_damage);
                }
                
                Debug.Log("DEAL WITH IT!");
                Destroy(gameObject);
            }
        }
    }

    public void Shot(float damage, float speed, Transform target)
    {
        Debug.Log("Shot!");
        _damage = damage;
        _speed = speed;
        _targetTransform = target;
        _isShot = true;
    }
}
