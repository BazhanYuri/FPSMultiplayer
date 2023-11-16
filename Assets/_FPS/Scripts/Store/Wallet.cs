using UnityEngine;

public class Wallet : IWallet
{
    public int Amount { get; private set; }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }
    public void AddAmount(int amount)
    {
        Amount += amount;
    }
    public void RemoveAmount(int amount)
    {
        Amount -= amount;
    }
    public bool CanAfford(int amount)
    {
        return Amount >= amount;
    }
}
