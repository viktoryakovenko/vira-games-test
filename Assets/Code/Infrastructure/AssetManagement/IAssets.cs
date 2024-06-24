using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        public GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path, Transform parent);
    }
}
