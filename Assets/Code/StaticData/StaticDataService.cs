using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public int PrizesCount => _prizes.Count;

        private List<PrizeStaticData> _prizes;

        public void LoadPrizes()
        {
            _prizes = Resources
                .LoadAll<PrizeStaticData>("StaticData/Prizes")
                .ToList();
        }

        public PrizeStaticData ForPrize(int id) =>
            _prizes.ElementAtOrDefault(id);
    }
}
