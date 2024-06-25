namespace Code.Infrastructure.Services.Randomizer
{
    public interface IRandomService : IService
    {
        float Next(float min, float max);
        int Next(int min, int max);
    }
}
