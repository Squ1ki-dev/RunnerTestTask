using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action OnGameOver;
    public static void Fire_OnGameOver() => OnGameOver?.Invoke();
}
