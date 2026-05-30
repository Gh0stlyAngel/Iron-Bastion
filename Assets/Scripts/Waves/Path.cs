using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public IReadOnlyList<Transform> Waypoints => _waypoints;

    private List<Transform> _waypoints = new();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            _waypoints.Add(child);
        }
    }
}
