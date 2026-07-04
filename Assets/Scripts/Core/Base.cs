using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private TextMeshProUGUI _baseHPText;

    private void Start()
    {
        _health = _maxHealth;

        if (_baseHPText != null)
            _baseHPText.text = $"{_health} / {_maxHealth}";
    }

    public void HandleEnemyReachedBase(Enemy enemy)
    {
        _health -= enemy.DamageToBase;
        _baseHPText.text = $"{_health} / {_maxHealth}";

        if (_health <= 0 )
        {
            _gameManager.Defeat();
        }
    }
}
