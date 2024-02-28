using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
    public delegate void OnGameOver();
    public static event OnGameOver onGameOver;
    public static void RaisOnGameOver() => onGameOver?.Invoke();
}
