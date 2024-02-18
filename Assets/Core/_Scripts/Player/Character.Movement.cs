using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character
{
    private void HandleMovement()
    {
        // Gravity
        Vector3 newVelocity = Velocity;
        if (_characterController.isGrounded)
            newVelocity.y = 0f;

        newVelocity.y += _gravity * Time.deltaTime;

        // Horizontal movement and jump input
        HandleHorizontalMovement();

        if (SwipeController.SwipeUp && _characterController.isGrounded)
        {
            newVelocity.y = _jumpVelocity;
            _animator.SetBool(_jumpId, true);
            StartCoroutine(StopJump(0.75f));
        }

        Velocity = newVelocity;
        Move(Velocity * Time.deltaTime);
    }

    private void HandleHorizontalMovement()
    {
        if (SwipeController.SwipeLeft && _movementLane > -1)
            MoveLane(-1);
        else if (SwipeController.SwipeRight && _movementLane < 1)
            MoveLane(1);
    }

    private void MoveLane(int direction)
    {
        StartCoroutine(HorizontalMove(direction));
        _movementLane += direction;
    }

    public void Move(Vector3 motion) => _characterController.Move(motion);

    private IEnumerator StopJump(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetBool(_jumpId, false);
    }

    private IEnumerator HorizontalMove(int direction)
    {
        float remainingOffset = _movementOffset;
        while (remainingOffset > 0f)
        {
            float movement = _horizontalSpeed * Time.deltaTime;
            movement = Mathf.Min(movement, remainingOffset);

            Move(new Vector3(movement * direction, 0f, 0f));

            remainingOffset -= movement;
            yield return null;
        }
    }
}
