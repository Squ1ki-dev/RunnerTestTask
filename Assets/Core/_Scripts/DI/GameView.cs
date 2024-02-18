using VContainer;
using VContainer.Unity;

public class GameView : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<SceneLoader>(Lifetime.Singleton).AsSelf();
        builder.Register<InitLoader>(Lifetime.Singleton).As<IAsyncStartable>();
    }
}
