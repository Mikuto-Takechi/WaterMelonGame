using VContainer.Unity;
using VContainer;

class MyLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // インスタンスを作ってもらう
        builder.Register<Display>(Lifetime.Singleton);
    }
}