using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory
    {
        GameObject CreateWheel(Transform at);
    }
}
