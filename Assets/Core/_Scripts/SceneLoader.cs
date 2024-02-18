using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class SceneLoader
{
    private readonly GameView _gameView;

    public SceneLoader(GameView gameView) => _gameView = gameView;

    public async UniTask LoadSceneAsync(int sceneIndex)
    {
        using (LifetimeScope.EnqueueParent(_gameView))
        {
            await SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
