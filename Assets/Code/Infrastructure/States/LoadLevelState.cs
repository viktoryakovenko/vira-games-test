using Code.Infrastructure.Factory;
using Code.Logic;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string ParentContainer = "GameplayContainer";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            _curtain = curtain;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitWheelGame();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitWheelGame()
        {
            Transform parentContainer = GameObject.FindWithTag(ParentContainer).transform;

            _gameFactory.CreateWheel(parentContainer);
            _gameFactory.CreateHUD(parentContainer);
        }
    }
}