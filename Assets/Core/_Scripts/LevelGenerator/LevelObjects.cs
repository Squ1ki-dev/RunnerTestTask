using System;
using UnityEngine;

[Serializable]
public class LevelObjects
{
    public GameObject Prefab;
    public int SpawnChance;
    public int MinimumInRow = 1;
    public int MaximumInRow = 1;
}
