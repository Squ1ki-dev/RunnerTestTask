using UnityEngine;
using VContainer;

public class TileGenerator : MonoBehaviour
{
    private const float GenerationAheadDistance = 200f;
    [SerializeField] private TilePart _roadPart;

    private float _lastRoadPieceZPosition;
    private MainCamera _mainCamera;

    [Inject]
    public void Construct(MainCamera mainCamera) => _mainCamera = mainCamera;
    private void Update() => RoadPiece();

    private void RoadPiece()
    {
        float generationDistance = _mainCamera.transform.position.z + GenerationAheadDistance;

        while (_lastRoadPieceZPosition < generationDistance)
        {
            Instantiate(_roadPart.Prefab, new Vector3(0f, -0.45f, _lastRoadPieceZPosition), Quaternion.identity);
            _lastRoadPieceZPosition += _roadPart.Distance;
        }
    }
}
