using System;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private int _goldAmount;
    public int GoldAmount => _goldAmount;

    public event Action<int> GoldAmountChanged;

    public void AddGold(int amount)
    {
        _goldAmount += amount;

        GoldAmountChanged?.Invoke(_goldAmount);
    }

    public void SpendGold(int amount)
    {
        if (_goldAmount < amount)
        {
            return;
        }

        _goldAmount -= amount;

        GoldAmountChanged?.Invoke(_goldAmount);
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        AddGold(enemy.GoldReward);
    }
}
