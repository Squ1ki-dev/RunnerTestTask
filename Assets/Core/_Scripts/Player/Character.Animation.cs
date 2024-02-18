using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character
{
    private void SetAnimations()
    {
        SetRunningAnim();
        SetFlyingAnim();
    } 
    private void SetDeadAnim() => _animator.SetBool(AnimatorHashIDs.DeadAnimId, true);
    private void SetRunningAnim() => _animator.SetFloat(AnimatorHashIDs.RunAnimId, Velocity.z);
    private void SetFlyingAnim() => _animator.SetBool(AnimatorHashIDs.FlyingAnimId, Position.y > 1f && !_animator.GetBool(AnimatorHashIDs.JumpAnimId));
}
