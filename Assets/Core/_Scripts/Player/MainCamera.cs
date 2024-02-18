using UnityEngine;
using VContainer;

/// <summary>
/// Follower script attached to a main <see cref="Camera"/>.<br/>
/// </summary>
/// <remarks>
/// Maintains an initial camera offset from character.
/// </remarks>
public class MainCamera : MonoBehaviour
{
    private IRunner _followTarget;
    private Vector3 _offset;
    private Camera _camera;

    [Inject]
    public void Construct(IRunner runner)
    {
        _followTarget = runner;
        _offset = transform.position - _followTarget.Position;
    }

    private void Awake() => _camera = GetComponent<Camera>();
    private void LateUpdate() => _camera.transform.position = _followTarget.Position + _offset;
}
