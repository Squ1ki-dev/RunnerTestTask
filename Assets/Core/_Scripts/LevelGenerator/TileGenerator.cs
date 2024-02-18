using UnityEngine;
using VContainer;

public class TileGenerator : MonoBehaviour
{
    private const float GenerationAheadDistance = 200f;
    [SerializeField] private TilePart _roadPart;

    private float _lastRoadPiecePos;
    private MainCamera _mainCamera;

    [Inject]
    public void Construct(MainCamera mainCamera) => _mainCamera = mainCamera;
    private void Update() => RoadPart();

    private void RoadPart()
    {
        float generationDistance = _mainCamera.transform.position.z + GenerationAheadDistance;

        while (_lastRoadPiecePos < generationDistance)
        {
            Instantiate(_roadPart.Prefab, new Vector3(0f, -0.45f, _lastRoadPiecePos), Quaternion.identity);
            _lastRoadPiecePos += _roadPart.Distance;
        }
    }
}
