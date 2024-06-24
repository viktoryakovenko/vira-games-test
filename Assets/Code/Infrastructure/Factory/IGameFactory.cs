using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateWheel(Transform parent);
        GameObject CreateHUD(Transform parent);
    }
}
