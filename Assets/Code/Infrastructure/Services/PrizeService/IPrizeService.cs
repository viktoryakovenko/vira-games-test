using System.Collections.Generic;
using Code.StaticData;

namespace Code.Infrastructure.Services.PrizeService
{
    public interface IPrizeService : IService
    {
        int PrizesCount { get; }
        IReadOnlyList<PrizeStaticData> GetRandomPrizesList(int count);
        PrizeStaticData GetRandomPrize();
    }
}
