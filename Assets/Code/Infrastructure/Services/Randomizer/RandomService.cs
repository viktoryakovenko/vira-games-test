using UnityEngine;

namespace Code.Infrastructure.Services.Randomizer
{
    public class RandomService : IRandomService
    {
        public float Next(float min, float max) =>
            Random.Range(min, max);
    }
}
