using System;
using MessagePipe;
using UniRx;
using VContainer;
using VContainer.Unity;

public class CoinsScoreText : IStartable, IDisposable
{
    private CoinsScoreTextView _coinsScoreTextView;
    private ISubscriber<CoinsScoreMessage> _scoreSubscriber;
    private CoinsScoreMessageFilter _scoreMessageFilter;

    private CompositeDisposable _disposable = new();

    [Inject]
    public void Construct(CoinsScoreTextView coinsScoreTextView, ISubscriber<CoinsScoreMessage> scoreSubscriber, CoinsScoreMessageFilter scoreMessageFilter)
    {
        _scoreSubscriber = scoreSubscriber;
        _scoreMessageFilter = scoreMessageFilter;
        _coinsScoreTextView = coinsScoreTextView;
    }

    public void Start() => Init();
    private void Init()
    {
        _scoreSubscriber.Subscribe(scoreMessage => _coinsScoreTextView.Text.text = scoreMessage.Score.ToString(),
            _scoreMessageFilter).AddTo(_disposable);
    }
    public void Dispose() => _disposable.Dispose();
}
