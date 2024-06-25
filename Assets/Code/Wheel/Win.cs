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
        public int PrizesCount => _prizesCount;

        private IReadOnlyList<PrizeStaticData> _prizes;
        private IPrizeService _prizeService;
        private IRandomService _randomService;
        private int _prizesCount;

        public void Initialize(IRandomService randomService, IPrizeService prizeService, int prizesCount)
        {
            _randomService = randomService;
            _prizeService = prizeService;
            _prizesCount = prizesCount;
            _prizes = _prizeService.GetRandomPrizesList(_prizesCount);
        }

        public int GetWinItem(out PrizeStaticData prize)
        {
            var prizeId = _randomService.Next(0, _prizes.Count);

            prize = _prizes[prizeId];
            _prizeService.TryRemovePrize(prize);

            _prizes = _prizeService.GetRandomPrizesList(_prizesCount);
            Debug.Log($"{prize.Prefab} | {prize.IsUnique} | {prize.Amount}");

            return prizeId;
        }
    }
}
