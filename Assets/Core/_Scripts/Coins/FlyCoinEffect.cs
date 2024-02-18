using UnityEngine;

public class FlyCoinEffect : IEffectBehaviour
{
    private const float FlyHeight = 2f;
    private const float FlyTweenSpeed = 6f;
    private const float MagnetRadius = 7f;
    private const float MagnetSpeed = 20f;
    private const float SpeedAdjustment = 5f;

    private readonly IRunner _runner;
    private readonly float _duration;

    private float _elapsedTime;

    public int StackingIdentifier { get; } = 2;
    public float TimeRemaining => 1f - _elapsedTime / _duration;
    public bool OutOfTime => _elapsedTime >= _duration;

    public FlyCoinEffect(IRunner runner, float duration)
    {
        _runner = runner;
        _duration = duration;
    }

    public void EffectUpdating(float deltaTime)
    {
        bool started = _elapsedTime == 0;

        _elapsedTime += deltaTime;

        if (started)
            _runner.Velocity = new Vector3(_runner.Velocity.x, _runner.Velocity.y, _runner.Velocity.z + SpeedAdjustment);

        _runner.Velocity = new Vector3(_runner.Velocity.x, 0f, _runner.Velocity.z);
        float characterToFlyHeightDifference = FlyHeight - _runner.Position.y;

        if (characterToFlyHeightDifference > 0)
        {
            float heightAdjustmentThisFrame = Mathf.Min(FlyTweenSpeed * Time.deltaTime, characterToFlyHeightDifference);
            _runner.Move(new Vector3(0f, heightAdjustmentThisFrame, 0f));
        }

        Collider[] overlapColliders = Physics.OverlapSphere(_runner.Position, MagnetRadius);
        foreach (Collider overlapCollider in overlapColliders)
        {
            DefaultCoin defaultCoinCoin = overlapCollider.GetComponent<DefaultCoin>();
            if(defaultCoinCoin == null) return;
            defaultCoinCoin.transform.position = Vector3.MoveTowards(defaultCoinCoin.transform.position, _runner.Position, MagnetSpeed * Time.deltaTime);
        }
    }

    public void EndEffect() => _runner.Velocity = new Vector3(_runner.Velocity.x, _runner.Velocity.y, _runner.Velocity.z - SpeedAdjustment);
}
