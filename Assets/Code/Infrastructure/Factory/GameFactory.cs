using Code.Infrastructure.AssetManagment;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateWheel(Transform at)
        {
            GameObject wheel = _assets.Instantiate(AssetPath.WheelPath, at.position);

            return wheel;
        }
    }
}
