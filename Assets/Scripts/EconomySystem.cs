using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomySystem : MonoBehaviour
{
    [SerializeField, Min(0)] private int startingBalance = 250;
    public int Balance { get; private set; }
    public event Action<int> BalanceChanged;

    private void Awake()
    {
        Balance = startingBalance;
    }
    public void AddMoney(int amount)
    {
        if (amount <= 0)
            return;
        Balance += amount;
        BalanceChanged?.Invoke(Balance);
    }
    public bool TrySpendMoney(int amount)
    {
        if (amount <= 0 || Balance < amount)
            return false;
        Balance -= amount;
        BalanceChanged?.Invoke(Balance);
        return true;
    }
    public void SetBalanceForLoad(int amount)
    {
        Balance = Mathf.Max(0, amount);
        BalanceChanged?.Invoke(Balance);
    }
}
