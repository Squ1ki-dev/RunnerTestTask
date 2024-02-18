using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

public class InitLoader : IAsyncStartable
{
    private SceneLoader _sceneLoader;

    private InitLoader(SceneLoader sceneLoader) => _sceneLoader = sceneLoader;

    public async UniTask StartAsync(CancellationToken cancellation)
    {
        await _sceneLoader.LoadSceneAsync(1);
    }
}
