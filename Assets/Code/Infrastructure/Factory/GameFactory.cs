using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.PrizeService;
using Code.StaticData;
using Code.UI;
using Code.Wheel;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IPrizeService _prizeService;
        private readonly IAssets _assets;

        private GameObject _wheel;

        public GameFactory(IAssets assets, IPrizeService prizeService)
        {
            _prizeService = prizeService;
            _assets = assets;
        }

        public GameObject CreateWheel(Transform parent)
        {
            _wheel = _assets.Instantiate(AssetPath.WheelPath, parent);
            var data = Resources.Load<SpinWheelStaticData>("StaticData/SpinWheelData");

            var spinWheel = _wheel.GetComponent<SpinWheel>();
            spinWheel.Initialize(data.SpinSpeed, data.StopSpeed);

            var spinChecker = _wheel.GetComponent<SpinChecker>();
            spinChecker.Initialize(spinWheel, data.SpinsCount);

            Transform wheelSectors = spinWheel.WheelSectors.transform;
            FillSectors(wheelSectors.transform, data.TotalItemPositions);

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

        private void FillSectors(Transform sectorContainer, int amount)
        {
            var randomPrizes = _prizeService.GetRandomPrizesList(amount);
            for (int i = 0; i < amount; i++)
            {
                GameObject sector = _assets.Instantiate(AssetPath.SectorPath, sectorContainer);
                float hue = (float)i / amount;
                float angle = i * 360.0f / amount;
                var image = sector.GetComponent<Image>();
                image.color = Color.HSVToRGB(hue, 1, 1);
                image.fillAmount = 1.0f / amount;
                sector.transform.rotation = Quaternion.Euler(0, 0, angle);

                SetSectorInfo(sector.transform, angle, randomPrizes[i]);
            }
        }

        private void SetSectorInfo(Transform sector, float angle, PrizeStaticData prizeData)
        {
            Debug.Log($"{angle/2} {prizeData.IsUnique}");
            Object.Instantiate(prizeData.Prefab, sector);
        }
    }
}
