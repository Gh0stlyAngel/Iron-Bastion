using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health;
    public float MaxHealth;
    public int DamageToBase;
    public int GoldReward;

    public event Action<Enemy> Died;

    private void Awake()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
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
