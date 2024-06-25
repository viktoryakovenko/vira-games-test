using Code.StaticData;

namespace Code.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        int PrizesCount { get; }
        void LoadPrizes();
        PrizeStaticData ForPrize(int id);
    }
}
