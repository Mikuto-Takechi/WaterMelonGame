using VContainer.Unity;
using VContainer;

class MyLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // �C���X�^���X������Ă��炤
        builder.Register<Display>(Lifetime.Singleton);
    }
}