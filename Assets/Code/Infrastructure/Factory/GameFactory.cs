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

        public GameObject CreateWheel(Transform at)
        {
            _wheel = _assets.Instantiate(AssetPath.WheelPath, at.position);
            var data = Resources.Load<SpinWheelStaticData>("StaticData/Prizes");

            var spinWheel = _wheel.GetComponent<SpinWheel>();
            spinWheel.Initialize(data.SpinSpeed, data.StopSpeed);

            GameObject wheelSectors = spinWheel.WheelSectors;

            return _wheel;
        }

        private void FillSectors(Transform sectorContainer, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject sector = _assets.Instantiate(AssetPath.SectorPath, sectorContainer);
                float pixelChannel = i * amount / 255f;
                sector.GetComponent<Image>().color = new Color(pixelChannel, pixelChannel, pixelChannel);
            }
        }
    }
}
