using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameOverCanvas : IStartable, IDisposable
{
    private Player _runner;
    private SceneLoader _sceneLoader;
    private GameOverView _gameOverView;
    private readonly CompositeDisposable _disposable = new();

    private const int RestartSceneIndex = 1;

    [Inject]
    public void Construct(GameOverView gameOverView, Player runner, SceneLoader sceneLoader)
    {
        _runner = runner;
        _sceneLoader = sceneLoader;
        _gameOverView = gameOverView;
    }

    public void Start() => Init();

    private void Init()
    {
        _gameOverView.Canvas.enabled = false;

        _gameOverView.RestartButton.OnClickAsObservable().Subscribe(async _ =>
        {
            await _sceneLoader.LoadSceneAsync(RestartSceneIndex);
        }).AddTo(_disposable);

        Observable.EveryUpdate()
            .First(_ => _runner.IsDead)
            .Subscribe(_ =>
            {
                _gameOverView.Canvas.enabled = true;
                _gameOverView.CoinsText.text = $"Coins: {_runner.Score}";

            }).AddTo(_disposable);
    }

    public void Dispose() => _disposable.Dispose();
}
