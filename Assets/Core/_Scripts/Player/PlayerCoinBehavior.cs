using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerCoinBehavior
{
    private Character _character;
    public PlayerCoinBehavior(Character character) => _character = character;

    public void UpdateCoinBehaviors()
    {
        // Iterate through effect behaviors in reverse order
        for (int i = _character.EffectBehaviors.Count - 1; i >= 0; i--)
        {
            IEffectBehaviour effect = _character.EffectBehaviors[i];
            effect.EffectUpdating(Time.deltaTime);

            // Remove expired effects
            if (effect.OutOfTime)
            {
                effect.EndEffect();
                _character.EffectBehaviors.RemoveAt(i);
            }
        }
    }
}