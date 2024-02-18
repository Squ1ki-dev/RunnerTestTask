using System.Collections;
using System.Collections.Generic;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(CharacterController))]
public partial class Character : MonoBehaviour, IRunner
{
    private readonly int _deadAnimId = Animator.StringToHash("Death");
    private readonly int _flyingAnimId = Animator.StringToHash("Flying");
    private readonly int _jumpId = Animator.StringToHash("Jump");
    private readonly int _runAnimId = Animator.StringToHash("Running");

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravity;
    [SerializeField] private float _movementOffset;
    [SerializeField] private float _horizontalSpeed;

    private CharacterController _characterController;
    private Animator _animator;
    private IPublisher<CoinsScoreMessage> _scorePublisher;

    private int _movementLane = 0;

    // IRunner implementation
    public Vector3 Position => transform.position;
    public Vector3 Velocity { get; set; }
    public bool IsDead { get; set; } = false;
    public ReactiveCollection<IEffectBehaviour> EffectBehaviors { get; } = new();
    public int Score { get; private set; }

    [Inject]
    public void Construct(IPublisher<CoinsScoreMessage> scorePublisher)
    {
        _scorePublisher = scorePublisher;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        Velocity = new Vector3(0f, 0f, _speed);
    }

    private void Update()
    {
        _animator.SetBool(_deadAnimId, IsDead);

        if (IsDead) return;

        UpdateCoinBehaviors();
        HandleMovement();

        _animator.SetFloat(_runAnimId, Velocity.z);
        _animator.SetBool(_flyingAnimId, Position.y > 1f && !_animator.GetBool(_jumpId));
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        _scorePublisher.Publish(new CoinsScoreMessage(Score));
    }
}
