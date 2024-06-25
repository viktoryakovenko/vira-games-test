using System;

namespace Code.Wheel
{
    public interface ISpinWheel
    {
        event Action OnSpinStarted;
        event Action OnSpinEnded;
        float InitialRotationSpeed { get; }
        int FullTurns { get; }
        void Spin();
    }
}
