using System;
using UnityEngine;

public class Wallet : IWallet
{
    public int Amount { get; private set; }

    public event Action<int> OnAmountChanged;

    public void SetAmount(int amount)
    {
        Amount = amount;
        UpdateMoney();
    }
    public void AddAmount(int amount)
    {
        Amount += amount;
        UpdateMoney();
    }
    public void RemoveAmount(int amount)
    {
        Amount -= amount;
        UpdateMoney();
    }
    public bool CanAfford(int amount)
    {
        return Amount >= amount;
    }
    private void UpdateMoney()
    {
        OnAmountChanged?.Invoke(Amount);
    }
}
