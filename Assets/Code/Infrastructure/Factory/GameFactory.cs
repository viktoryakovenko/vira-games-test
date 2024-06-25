using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.PrizeService;
using Code.Infrastructure.Services.Randomizer;
using Code.StaticData;
using Code.UI;
using Code.Wheel;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IRandomService _randomService;
        private readonly IPrizeService _prizeService;
        private readonly IAssets _assets;

        private GameObject _wheel;

        public GameFactory(IRandomService randomService, IAssets assets, IPrizeService prizeService)
        {
            _randomService = randomService;
            _prizeService = prizeService;
            _assets = assets;
        }

        public GameObject CreateWheel(Transform parent)
        {
            _wheel = _assets.Instantiate(AssetPath.WheelPath, parent);
            var data = Resources.Load<SpinWheelStaticData>("StaticData/SpinWheelData");

            var spinWheel = _wheel.GetComponent<SpinWheel>();
            spinWheel.Initialize(_randomService, data.SpinSpeed, data.FullTurns);

            var spinChecker = _wheel.GetComponent<SpinChecker>();
            spinChecker.Initialize(spinWheel, data.SpinsCount);

            var win = _wheel.GetComponent<Win>();
            win.Initialize(_randomService, _prizeService, data.TotalItemPositions);

            var filler = _wheel.GetComponent<WheelFiller>();
            filler.Initialize(_assets);
            filler.FillSectors(filler.SectorContainer, win.Prizes);

            return _wheel;
        }

        public GameObject CreateHUD(Transform parent)
        {
            var hud = _assets.Instantiate(AssetPath.HUDPath, parent);

            var hudHandler = hud.GetComponent<HUDHandler>();
            var spinCommand = hudHandler.SpinCommand;
            spinCommand.Initialize(_wheel.GetComponent<ISpinWheel>(), _wheel.GetComponent<ISpinChecker>());

            var counter = hudHandler.Counter;
            counter.Initialize(_wheel.GetComponent<ISpinChecker>() );

            return hud;
        }
    }
}
