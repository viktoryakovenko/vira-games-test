using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.PrizeService;
using Code.Infrastructure.Services.Randomizer;
using Code.StaticData;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevel() => _stateMachine.Enter<LoadLevelState, string>("Gameplay");

        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IPrizeService>(new PrizeService(_services.Single<IStaticDataService>(), _services.Single<IRandomService>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IRandomService>(),_services.Single<IAssets>(), _services.Single<IPrizeService>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadPrizes();
            _services.RegisterSingle(staticData);
        }
    }
}