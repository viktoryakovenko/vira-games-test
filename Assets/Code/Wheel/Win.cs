using System.Collections.Generic;
using Code.Infrastructure.Services.PrizeService;
using Code.Infrastructure.Services.Randomizer;
using Code.StaticData;
using UnityEngine;

namespace Code.Wheel
{
    public class Win : MonoBehaviour
    {
        public IReadOnlyList<PrizeStaticData> Prizes => _prizes;
        public int PrizesCount => _prizes.Count;

        private IReadOnlyList<PrizeStaticData> _prizes;
        private IRandomService _randomService;
        public void Initialize(IRandomService randomService, IPrizeService prizeService, int prizesCount)
        {
            _randomService = randomService;
            _prizes = prizeService.GetRandomPrizesList(prizesCount);
        }

        public int GetWinItemId()
        {
            var id = _randomService.Next(0, _prizes.Count);
            Debug.Log($"{_prizes[id].Prefab} | {_prizes[id].Amount}");
            return id;
        }
    }
}
