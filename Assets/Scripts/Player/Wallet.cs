using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<float> ValueChanged;

    public float Value { get; private set; }

    public void AddCoin()
    {
        Value++;
        ValueChanged?.Invoke(Value);
    }
}