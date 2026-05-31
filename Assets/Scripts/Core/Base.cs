using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _health;

    public void HandleEnemyReachedBase(Enemy enemy)
    {
        _health -= enemy.DamageToBase;
    }
}
