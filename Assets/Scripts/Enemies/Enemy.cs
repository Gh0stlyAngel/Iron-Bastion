using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    private float _health;
    public float MaxHealth;
    public int DamageToBase;
    public int GoldReward;
    public int Armor;

    public event Action<Enemy> Died;

    private const float ArmorReductionPerPoint = 0.025f;

    private void Awake()
    {
        _health = MaxHealth;
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                float damageReduction = Armor * ArmorReductionPerPoint;
                _health -= damage * (1f - damageReduction);
                break;

            case DamageType.Magic:
                _health -= damage;
                break;

            default:
                Debug.LogWarning($"Unknown damage type: {damageType}");
                break;
        }

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    public void OnReachedBase(Enemy _)
    {
        Destroy(gameObject);
    }
}
