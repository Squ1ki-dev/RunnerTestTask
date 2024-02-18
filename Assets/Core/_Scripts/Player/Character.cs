using System.Collections;
using System.Collections.Generic;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(CharacterController))]
public partial class Character : MonoBehaviour, IRunner
{
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
    public void Construct(IPublisher<CoinsScoreMessage> scorePublisher) => _scorePublisher = scorePublisher;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        Velocity = new Vector3(0f, 0f, _speed);
    }

    private void Update()
    {
        if (IsDead)
        {
            SetDeadAnim();
            return;
        }

        UpdateCoinBehaviors();
        HandleMovement();
        SetAnimations();
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        _scorePublisher.Publish(new CoinsScoreMessage(Score));
    }
}
