using System;

namespace Code.UI
{
    public interface ISpinWheel
    {
        event Action OnSpinStarted;
        event Action OnSpinEnded;
        float InitialRotationSpeed { get; }
        float DecelerationSpeed { get; }
        void Spin();
    }
}
