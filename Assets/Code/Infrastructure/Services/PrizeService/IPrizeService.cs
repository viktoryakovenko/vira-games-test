using System.Collections.Generic;
using Code.StaticData;

namespace Code.Infrastructure.Services.PrizeService
{
    public interface IPrizeService : IService
    {
        IReadOnlyList<PrizeStaticData> GetRandomPrizesList(int count);
        void TryRemovePrize(PrizeStaticData prize);
    }
}
