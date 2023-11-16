using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWallet 
{
    int Amount { get; }
    void SetAmount(int amount);
    void AddAmount(int amount);
    void RemoveAmount(int amount);
    bool CanAfford(int amount);
}
