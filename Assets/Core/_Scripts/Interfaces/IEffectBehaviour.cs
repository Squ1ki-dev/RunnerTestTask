using UnityEngine;

public interface IEffectBehaviour
{
    int StackingIdentifier { get; }
    float TimeRemaining { get; }
    bool OutOfTime { get; }
    void EffectUpdating(float deltaTime);
    void EndEffect();
}
