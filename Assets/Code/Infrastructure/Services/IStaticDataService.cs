namespace Code.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadPrizes();
    }
}
