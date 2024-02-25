using Zenject;


namespace Vald
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSceneData>().FromNew().AsSingle();
        }
    }
}
