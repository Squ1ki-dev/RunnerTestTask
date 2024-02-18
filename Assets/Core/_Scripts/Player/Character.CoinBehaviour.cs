using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character
{
    private void UpdateCoinBehaviors()
    {
        // Support for stacking coin behaviors
        for (int effectIteration = EffectBehaviors.Count - 1; effectIteration >= 0; effectIteration--)
        {
            IEffectBehaviour effectBehaviour = EffectBehaviors[effectIteration];
            effectBehaviour.EffectUpdating(Time.deltaTime);

            if (effectBehaviour.OutOfTime)
            {
                effectBehaviour.EndEffect();
                EffectBehaviors.RemoveAt(effectIteration);
            }
        }
    }

    public void AddEffect(IEffectBehaviour effectBehaviour)
    {
        for (int effectIteration = EffectBehaviors.Count - 1; effectIteration >= 0; effectIteration--)
        {
            IEffectBehaviour existingEffectBehaviour = EffectBehaviors[effectIteration];
            if (existingEffectBehaviour.StackingIdentifier == effectBehaviour.StackingIdentifier)
            {
                existingEffectBehaviour.EndEffect();
                EffectBehaviors.RemoveAt(effectIteration);
            }
        }

        EffectBehaviors.Add(effectBehaviour);
    }
}
