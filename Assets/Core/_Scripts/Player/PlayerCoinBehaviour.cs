using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerCoinBehaviour : MonoBehaviour, IEffectTarget
{
    public ReactiveCollection<IEffectBehaviour> EffectBehaviors { get; } = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
