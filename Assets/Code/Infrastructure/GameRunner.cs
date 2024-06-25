using UnityEngine;

namespace Code.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private void Awake()
        {
            var bootstraper = FindObjectOfType<GameBootstrapper>();

            if (bootstraper == null)
            {
                Instantiate(BootstrapperPrefab);
            }
        }
    }
}