using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _health;

    public void HandleEnemyReachedBase()
    {
        _health -= 1;
        Debug.Log($"Current health: {_health}");
    }
}
