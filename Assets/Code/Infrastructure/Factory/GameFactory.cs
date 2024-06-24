using Code.Infrastructure.AssetManagement;
using Code.StaticData;
using Code.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        private GameObject _wheel;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateWheel(Transform parent)
        {
            _wheel = _assets.Instantiate(AssetPath.WheelPath, parent);
            var data = Resources.Load<SpinWheelStaticData>("StaticData/SpinWheelData");

            var spinWheel = _wheel.GetComponent<SpinWheel>();
            spinWheel.Initialize(data.SpinSpeed, data.StopSpeed);

            Transform wheelSectors = spinWheel.WheelSectors.transform;
            FillSectors(wheelSectors.transform, data.TotalItemPositions);

            return _wheel;
        }

        public GameObject CreateSpinButton(Transform parent)
        {
            var spinButton = _assets.Instantiate(AssetPath.SpinButtonPath, parent);

            var spinCommand = spinButton.GetComponent<SpinCommand>();
            spinCommand.Initialize(_wheel.GetComponent<SpinWheel>());

            return spinButton;
        }

        private void FillSectors(Transform sectorContainer, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject sector = _assets.Instantiate(AssetPath.SectorPath, sectorContainer);
                float hue = (float)i / amount;
                float angle = i * 360.0f / amount;
                sector.GetComponent<Image>().color = Color.HSVToRGB(hue, 100, 100);
                sector.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
