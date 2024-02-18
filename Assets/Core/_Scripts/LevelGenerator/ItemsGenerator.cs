using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = System.Random;

/// <summary>
/// Infinite runner level generator. Generates pickups and obstacles at run time based on camera position.
/// </summary>
/// <remarks>
/// Supports creating additional coins without modifying the source code.
/// </remarks>
public class ItemsGenerator : MonoBehaviour
{
    private const float GenerationAheadDistance = 100f;
    private const float DistanceBetweenObjects = 2f;
    private const float HorizontalOffset = 2.2f;

    private MainCamera _mainCamera;
    private IObjectResolver _objectResolver;

    [SerializeField] private List<LevelObject> _levelObjects = new();

    private Random _random = new();
    private LevelObject _lastSelectedLevelObject;

    private int _totalPickupRoll;
    private float _lastObjectZPosition = 20f;
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
        foreach (LevelObject levelPickup in _levelObjects)
            _totalPickupRoll += levelPickup.SpawnChance;
    }

    private void Update()
    {
        GenerateRoad();
    }

    private void GenerateRoad()
    {
        float generationDistance = _mainCamera.transform.position.z + GenerationAheadDistance;

        while (_lastObjectZPosition < generationDistance)
        {
            LevelObject levelObject = SelectRandomObject();
            int amount = _random.Next(levelObject.MinimumInRow, levelObject.MaximumInRow);

            int lane = GetRandomLane();
            _lastLane = lane;

            while (amount > 0)
            {
                float zPosition = _lastObjectZPosition + DistanceBetweenObjects;

                if (levelObject.Prefab != null)
                    SpawnLevelObject(levelObject.Prefab, lane, zPosition);

                amount--;
                _lastObjectZPosition = zPosition;
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

    private LevelObject SelectRandomObject()
    {
        LevelObject selectedObject = null;

        do
        {
            float pickupRoll = _random.Next(0, _totalPickupRoll);
            float minimumRollToSelect = 0;

            foreach (LevelObject levelObject in _levelObjects)
            {
                if (pickupRoll <= levelObject.SpawnChance + minimumRollToSelect)
                {
                    selectedObject = levelObject;
                    break;
                }

                minimumRollToSelect += levelObject.SpawnChance;
            }
        }
        while (_lastSelectedLevelObject == selectedObject);

        _lastSelectedLevelObject = selectedObject;

        return selectedObject;
    }
}
