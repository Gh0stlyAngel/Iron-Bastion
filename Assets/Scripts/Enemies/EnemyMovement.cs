using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private Enemy _enemy;
    private int _currentWaypoint;
    private Vector3 Waypoint => _path.Waypoints[_currentWaypoint].position;
    private bool HasReachedEnd => _currentWaypoint >= _path.Waypoints.Count;

    [SerializeField] private float _speed;
    [SerializeField] private float _reachDistance = 0.1f;

    public event Action<Enemy> ReachedBase;

    public void Initialize(Path path)
    {
        _path = path;
        _currentWaypoint = 0;
        RotateToCurrentWaypoint();
    }

    private void Update()
    {
        if(HasReachedEnd)
        {
            ReachBase();
            return;
        }
            

        if (HasReachedWaypoint())
        {
            AdvanceWaypoint();

            if (HasReachedEnd)
                return;
        }

        MoveToWaypoint();

    }

    private void RotateToCurrentWaypoint()
    {
        if (HasReachedEnd)
        {
            return;
        }
        Vector3 direction = Waypoint - transform.position;
        direction.y = 0;
        direction = direction.normalized;

        transform.rotation = Quaternion.LookRotation(direction);
    }

    private bool HasReachedWaypoint()
    {
        return Vector3.Distance(transform.position, Waypoint) <= _reachDistance;
    }

    private void MoveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, Waypoint, _speed * Time.deltaTime);
    }

    private void AdvanceWaypoint()
    {
        _currentWaypoint++;

        if (!HasReachedEnd)
        {
            RotateToCurrentWaypoint();
        }
    }

    private void ReachBase()
    {
        ReachedBase?.Invoke(_enemy);
    }
}
