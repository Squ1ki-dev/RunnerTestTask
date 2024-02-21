using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinEffectSO")]
public class CoinEffectSO : ScriptableObject
{
    public float FlyHeight = 2f;
    public float FlyTweenSpeed = 6f;
    public float MagnetRadius = 7f;
    public float MagnetSpeed = 20f;
    public float SpeedAdjustment = 5f;
}
