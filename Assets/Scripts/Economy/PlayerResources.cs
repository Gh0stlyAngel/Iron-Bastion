using System;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] private int _goldAmount;
    public int GoldAmount => _goldAmount;

    public event Action<int> GoldAmountChanged;

    private void Start()
    {
        GoldAmountChanged?.Invoke(_goldAmount);
    }

    public void AddGold(int amount)
    {
        _goldAmount += amount;

        GoldAmountChanged?.Invoke(_goldAmount);
    }

    public bool TrySpendGold(int amount)
    {
        if (_goldAmount < amount)
        {
            return false;
        }

        _goldAmount -= amount;

        GoldAmountChanged?.Invoke(_goldAmount);

        return true;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        AddGold(enemy.GoldReward);
    }
}
