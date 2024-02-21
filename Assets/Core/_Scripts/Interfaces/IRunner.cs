using UniRx;
using UnityEngine;

public interface IRunner
{
    Vector3 Position { get; }
    Vector3 Velocity { get; set; }
    void Move(Vector3 motion);
}
