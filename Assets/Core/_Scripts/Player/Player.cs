using System.Collections;
using System.Collections.Generic;
using MessagePipe;
using UniRx;
using VContainer;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public partial class Player : MonoBehaviour, IRunner
{
    private int _movementLane = 0;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravity;
    [SerializeField] private float _movementOffset;
    [SerializeField] private float _horizontalSpeed;

    private AnimationController _animController;
    private PlayerCoinBehavior _coinBehaviorManager;
    private CharacterController _characterController;
    private Animator _animator;

    public Vector3 Position => transform.position;
    public Vector3 Velocity { get; set; }
    public bool IsDead { get; set; } = false;
    public int Score { get; private set; }
    public ReactiveCollection<IEffectBehaviour> EffectBehaviors { get; } = new();
    private IPublisher<CoinsScoreMessage> _scorePublisher;

    [Inject]
    public void Construct(IPublisher<CoinsScoreMessage> scorePublisher) => _scorePublisher = scorePublisher;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animController = GetComponent<AnimationController>();
        _coinBehaviorManager = new PlayerCoinBehavior(this);
        _animator = GetComponentInChildren<Animator>();

        Velocity = new Vector3(0f, 0f, _speed);
    }

    private void OnEnable() => EventManager.onGameOver += SetDeadAnim;
    private void OnDisable() => EventManager.onGameOver -= SetDeadAnim;
    public void SetDeadAnim() => _animator.SetBool(AnimatorHashIDs.DeadAnimId, true);

    private void Update()
    {
        _coinBehaviorManager.UpdateCoinBehaviors();
        HandleMovement();
        _animController.SetAnimations();
    }

    public void AddEffect(IEffectBehaviour newEffect)
    {
        RemoveExistingEffects(newEffect.StackingIdentifier);
        EffectBehaviors.Add(newEffect);
    }

    private void RemoveExistingEffects(int stackingIdentifier)
    {
        for (int i = EffectBehaviors.Count - 1; i >= 0; i--)
        {
            IEffectBehaviour existingEffect = EffectBehaviors[i];
            if (existingEffect.StackingIdentifier == stackingIdentifier)
            {
                existingEffect.EndEffect();
                EffectBehaviors.RemoveAt(i);
            }
        }
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        _scorePublisher.Publish(new CoinsScoreMessage(Score));
    }

    private void HandleMovement()
    {
        // Gravity
        Vector3 newVelocity = Velocity;
        if (_characterController.isGrounded)
            newVelocity.y = 0f;

        newVelocity.y += _gravity * Time.deltaTime;

        HandleHorizontalMovement();

        if (SwipeController.SwipeUp && _characterController.isGrounded)
        {
            newVelocity.y = _jumpVelocity;
            _animController.SetJumpAnim(true);
            StartCoroutine(FinishJump(0.75f));
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

    private IEnumerator FinishJump(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animController.SetJumpAnim(false);
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