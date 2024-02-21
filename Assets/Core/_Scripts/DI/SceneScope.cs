using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneScope : LifetimeScope
{
    [SerializeField] private MainCamera _mainCamera;
    [SerializeField] private Player _playerCharacter;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private CoinsScoreTextView _coinsScoreTextView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_mainCamera).AsSelf();
        builder.RegisterInstance(_playerCharacter).As<IRunner>();
        builder.RegisterInstance(_gameOverView).AsSelf();
        builder.RegisterInstance(_coinsScoreTextView).AsSelf();

        builder.Register<GameOverCanvas>(Lifetime.Singleton).As<IStartable>();
        builder.Register<CoinsScoreText>(Lifetime.Singleton).As<IStartable>();

        MessagePipeOptions messagePipeOptions = builder.RegisterMessagePipe();

        builder.RegisterMessageHandlerFilter<CoinsScoreMessageFilter>();
        builder.RegisterMessageBroker<CoinsScoreMessage>(messagePipeOptions);
    }
}
