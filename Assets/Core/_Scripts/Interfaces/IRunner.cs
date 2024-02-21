using UniRx;
using UnityEngine;

public interface IRunner : IEffectTarget
{
    Vector3 Position { get; }
    Vector3 Velocity { get; set; }
    void Move(Vector3 motion);
}
