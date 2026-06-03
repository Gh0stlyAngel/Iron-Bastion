using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private GameManager _gameManager; 

    public void HandleEnemyReachedBase(Enemy enemy)
    {
        _health -= enemy.DamageToBase;

        if (_health <= 0 )
        {
            _gameManager.Defeat();
        }
    }
}
