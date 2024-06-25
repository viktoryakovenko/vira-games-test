using System;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Services.Randomizer;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Services.PrizeService
{
    public class PrizeService : IPrizeService
    {
        private readonly IStaticDataService _dataService;
        private readonly IRandomService _randomService;

        public int PrizesCount => _allPrizes.Count;

        private List<PrizeStaticData> _currentPrizes = new List<PrizeStaticData>();
        private List<PrizeStaticData> _allPrizes = new List<PrizeStaticData>();

        public PrizeService(IStaticDataService dataService, IRandomService randomService)
        {
            _dataService = dataService;
            _randomService = randomService;
            for (int i = 0; i < _dataService.PrizesCount; i++)
            {
                _allPrizes.Add(_dataService.ForPrize(i));
            }
        }

        public IReadOnlyList<PrizeStaticData> GetRandomPrizesList(int count) =>
            _allPrizes
                .OrderBy(x => _randomService.Next(0, _allPrizes.Count))
                .Take(count)
                .ToList();
    }
}
