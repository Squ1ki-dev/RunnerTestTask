using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoinEffectBase<T> : MonoBehaviour
{
    [SerializeField] protected T _coinData;

    protected abstract void ApplyEffect(IRunner runner);
}
