using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using MessagePipe;
using UniRx;

public class ScoreController : MonoBehaviour
{
    private IPublisher<CoinsScoreMessage> _scorePublisher;
    public int Score { get; private set; }
    public bool IsDead { get; set; } = false;

    [Inject]
    public void Construct(IPublisher<CoinsScoreMessage> scorePublisher) => _scorePublisher = scorePublisher;
    public void IncreaseScore(int amount)
    {
        Score += amount;
        _scorePublisher.Publish(new CoinsScoreMessage(Score));
    }
}
