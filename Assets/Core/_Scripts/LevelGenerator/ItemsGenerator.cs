using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = System.Random;

public class ItemsGenerator : MonoBehaviour
{
    private const float GenerationAheadDistance = 100f;
    private const float DistanceBetweenObjects = 2f;
    private const float HorizontalOffset = 2.2f;

    private MainCamera _mainCamera;
    private IObjectResolver _objectResolver;

    [SerializeField] private List<LevelObjects> _levelObjects = new();

    private Random _random = new();
    private LevelObjects _lastSelectedLevelObjects;

    private int _totalPickupRoll;
    private float _lastObjectPos = 20f;
    private int _lastLane;

    [Inject]
    public void Construct(MainCamera mainCamera, IObjectResolver objectResolver)
    {
        _mainCamera = mainCamera ?? throw new ArgumentNullException(nameof(mainCamera));
        _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
    }

    private void Awake() => TotalPickupRoll();

    private void TotalPickupRoll()
    {
        foreach (LevelObjects levelPickup in _levelObjects)
            _totalPickupRoll += levelPickup.SpawnChance;
    }

    private void Update()
    {
        GenerateRoad();
    }

    private void GenerateRoad()
    {
        float generationDistance = _mainCamera.transform.position.z + GenerationAheadDistance;

        while (_lastObjectPos < generationDistance)
        {
            LevelObjects levelObjects = SelectRandomObjects();
            int amount = _random.Next(levelObjects.MinimumInRow, levelObjects.MaximumInRow);

            int lane = GetRandomLane();
            _lastLane = lane;

            while (amount > 0)
            {
                float zPosition = _lastObjectPos + DistanceBetweenObjects;

                if (levelObjects.Prefab != null)
                    SpawnLevelObject(levelObjects.Prefab, lane, zPosition);

                amount--;
                _lastObjectPos = zPosition;
            }
        }
    }

    private int GetRandomLane()
    {
        int lane;
        do
        {
            lane = _random.Next(-1, 2);
        }
        while (lane == _lastLane);
        return lane;
    }

    private void SpawnLevelObject(GameObject prefab, int lane, float zPosition)
    {
        GameObject levelGameObject = _objectResolver.Instantiate(prefab);
        levelGameObject.transform.position = new Vector3(lane * HorizontalOffset, 0f, zPosition);
    }

    private LevelObjects SelectRandomObjects()
    {
        LevelObjects selectedObject = null;

        do
        {
            float pickupRoll = _random.Next(0, _totalPickupRoll);
            float minimumRollToSelect = 0;

            foreach (LevelObjects levelObjects in _levelObjects)
            {
                if (pickupRoll <= levelObjects.SpawnChance + minimumRollToSelect)
                {
                    selectedObject = levelObjects;
                    break;
                }

                minimumRollToSelect += levelObjects.SpawnChance;
            }
        }
        while (_lastSelectedLevelObjects == selectedObject);

        _lastSelectedLevelObjects = selectedObject;

        return selectedObject;
    }
}
