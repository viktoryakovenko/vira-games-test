using System;

namespace Code.Wheel
{
    public interface ISpinChecker
    {
        event Action OnCountChanged;
        bool CanSpin { get; }
        int TotalSpins { get; }
    }
}
