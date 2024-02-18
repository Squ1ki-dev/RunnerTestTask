using UnityEngine;

// Effects from this coin can be stacked, each effect has its own duration.
public class ChangeSpeedCoinEffect : IEffectBehaviour
{
    private readonly IRunner _runner;
    private readonly float _duration;
    private readonly float _speedAdjustment;

    private float _elapsedTime;
    public int StackingIdentifier { get; } = 1;
    public float TimeRemaining => 1f - _elapsedTime / _duration;
    public bool OutOfTime => _elapsedTime >= _duration;

    public ChangeSpeedCoinEffect(IRunner runner, float speedAdjustment, float duration)
    {
        _runner = runner;
        _duration = duration;
        _speedAdjustment = speedAdjustment;
    }

    public void EffectUpdating(float deltaTime)
    {
        _elapsedTime += deltaTime;

        if (_elapsedTime == 0)
            _runner.Velocity = new Vector3(_runner.Velocity.x, _runner.Velocity.y, _runner.Velocity.z + _speedAdjustment);
    }

    public void EndEffect() => _runner.Velocity = new Vector3(_runner.Velocity.x, _runner.Velocity.y, _runner.Velocity.z - _speedAdjustment);
}
