using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Services.PrizeService;
using Code.Infrastructure.Services.Randomizer;
using Code.StaticData;
using Code.UI;
using Code.Wheel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

            Transform wheelSectors = spinWheel.WheelSectors.transform;
            FillSectors(wheelSectors.transform, win.Prizes);

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

        private void FillSectors(Transform sectorContainer, IReadOnlyList<PrizeStaticData> randomPrizes)
        {
            for (int i = 0; i < randomPrizes.Count; i++)
            {
                GameObject sector = _assets.Instantiate(AssetPath.SectorPath, sectorContainer);
                float hue = (float)i / randomPrizes.Count;
                float angle = i * 360.0f / randomPrizes.Count;
                var image = sector.GetComponent<Image>();
                image.color = Color.HSVToRGB(hue, 1, 1);
                image.fillAmount = 1.0f / randomPrizes.Count;
                sector.transform.rotation = Quaternion.Euler(0, 0, angle);

                SetSectorInfo(sector.transform, image.fillAmount, randomPrizes[i]);
            }
        }

        private void SetSectorInfo(Transform sector, float fillAngle, PrizeStaticData prizeData)
        {
            var radians = (fillAngle - fillAngle / 2.0f) * 2.0f * Mathf.PI;
            var delta = Mathf.Sqrt(1000f * fillAngle);
            float radius = (sector.GetComponent<RectTransform>().sizeDelta.x) / 2.0f - delta;

            float x = radius * Mathf.Cos(radians);
            float y = radius * Mathf.Sin(radians);

            var prizeIcon = Object.Instantiate(prizeData.Prefab, sector);
            prizeIcon.transform.localPosition = new Vector3(-x, y, 0);
            prizeIcon.transform.localRotation = Quaternion.Euler(0, 0, 90 - radians * 180.0f / Mathf.PI);

            if (prizeData.Amount > 1 && !prizeData.IsUnique)
            {
                SetItemText(sector, prizeData, radius, radians);
            }
        }

        private static void SetItemText(Transform sector, PrizeStaticData prizeData, float radius, float radians)
        {
            float delta = 0.65f * radius;
            float x = delta * Mathf.Cos(radians);
            float y = delta * Mathf.Sin(radians);

            var rect = new GameObject("[Text]").AddComponent<RectTransform>();
            rect.transform.SetParent(sector);
            rect.transform.localPosition = new Vector3(-x, y);
            rect.transform.localRotation = Quaternion.Euler(0, 0, 90 - radians * 180.0f / Mathf.PI);
            rect.transform.localScale = Vector3.one;

            var text = rect.gameObject.AddComponent<TextMeshProUGUI>();
            text.text = prizeData.Amount.ToString();
            text.fontSize = 7f;
            text.enableWordWrapping = false;
            text.alignment = TextAlignmentOptions.Center;
        }
    }
}
