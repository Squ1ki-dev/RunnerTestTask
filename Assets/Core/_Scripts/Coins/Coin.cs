using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Coin<T> : MonoBehaviour where T : CoinEffect
{
    protected abstract void SelectCoin(Collider collider);

    private void OnTriggerEnter(Collider collider)
    {
        SelectCoin(collider);
    }
}