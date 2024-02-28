using UnityEngine;

public class FlyCoinEffect : CoinEffect, IEffectBehaviour
{
    private float _flyHeight;
    private float _flyTweenSpeed;
    private float _speedAdjustment;

    private float _elapsedTime;

    public int StackingIdentifier { get; } = 2;
    public float TimeRemaining => 1f - _elapsedTime / _duration;
    public bool OutOfTime => _elapsedTime >= _duration;

    public FlyCoinEffect(IRunner runner, CoinEffectSO config) : base(runner, config)
    {
        _runner = runner;
        _flyHeight = config.FlyHeight;
        _flyTweenSpeed = config.FlyTweenSpeed;
        _speedAdjustment = config.SpeedAdjustment;
        _duration = config.Duration;
    }

    public void EffectUpdating(float deltaTime)
    {
        bool started = _elapsedTime == 0;

        _elapsedTime += deltaTime;

        if (started)
            _runner.Velocity = new Vector3(_runner.Velocity.x, _runner.Velocity.y, _runner.Velocity.z + _speedAdjustment);

        _runner.Velocity = new Vector3(_runner.Velocity.x, 0f, _runner.Velocity.z);
        float characterToFlyHeightDifference = _flyHeight - _runner.Position.y;

        if (characterToFlyHeightDifference > 0)
        {
            float heightAdjustmentThisFrame = Mathf.Min(_flyTweenSpeed * Time.deltaTime, characterToFlyHeightDifference);
            _runner.Move(new Vector3(0f, heightAdjustmentThisFrame, 0f));
        }
    }

    public void EndEffect() => _runner.Velocity = new Vector3(_runner.Velocity.x, _runner.Velocity.y, _runner.Velocity.z - _speedAdjustment);
}
