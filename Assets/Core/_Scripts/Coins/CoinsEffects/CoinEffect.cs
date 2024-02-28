using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoinEffect
{
    public IRunner _runner;
    public float _duration = 10f;

    public CoinEffect(IRunner runner, CoinSO config)
    {
        _runner = runner;
        _duration = config.Duration;
    }
}
