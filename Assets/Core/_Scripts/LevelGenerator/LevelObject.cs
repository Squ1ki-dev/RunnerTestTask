using System;
using UnityEngine;

[Serializable]
public class LevelObject
{
    public GameObject Prefab;
    public int SpawnChance;
    public int MinimumInRow = 1;
    public int MaximumInRow = 1;
}
