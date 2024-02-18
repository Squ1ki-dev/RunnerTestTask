using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class SceneLoader
{
    private readonly GameLifetime _gameLifetime;

    public SceneLoader(GameLifetime gameLifetime) => _gameLifetime = gameLifetime;

    public async UniTask LoadSceneAsync(int sceneIndex)
    {
        using (LifetimeScope.EnqueueParent(_gameLifetime))
        {
            await SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
