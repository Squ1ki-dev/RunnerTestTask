using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePipe;
using UniRx;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    public Vector3 Velocity { get; set; }

    private void Awake() => _animator = GetComponentInChildren<Animator>();

    public void SetAnimations()
    {
        SetRunningAnim();
        SetFlyingAnim();
    }

    public void SetDeadAnim() => _animator.SetBool(AnimatorHashIDs.DeadAnimId, true);
    private void SetRunningAnim() => _animator.SetFloat(AnimatorHashIDs.RunAnimId, Velocity.z);
    private void SetFlyingAnim() => _animator.SetBool(AnimatorHashIDs.FlyingAnimId, transform.position.y > 1f && !_animator.GetBool(AnimatorHashIDs.JumpAnimId));
    public void SetJumpAnim(bool isPlay) => _animator.SetBool(AnimatorHashIDs.JumpAnimId, isPlay);
}