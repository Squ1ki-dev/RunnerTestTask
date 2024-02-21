// Used for adding pickup effects to implementing class.
using UniRx;
public interface IEffectTarget
{
    void AddEffect(IEffectBehaviour effectBehaviour);
    ReactiveCollection<IEffectBehaviour> EffectBehaviors { get; }
}
