using UnityEngine;

namespace Code.UI
{
    [RequireComponent(typeof(ButtonHandler))]
    public class SpinCommand : MonoBehaviour, ICommand
    {
        private SpinWheel _spinWheel;
        private ButtonHandler _buttonHandler;

        public void Initialize(SpinWheel wheel)
        {
            _spinWheel = wheel;
            _buttonHandler = GetComponent<ButtonHandler>();
            _spinWheel.OnSpinStarted += _buttonHandler.DisableButton;
            _spinWheel.OnSpinEnded += _buttonHandler.EnableButton;
        }

        private void OnDestroy()
        {
            _spinWheel.OnSpinStarted -= _buttonHandler.DisableButton;
            _spinWheel.OnSpinEnded -= _buttonHandler.EnableButton;
        }

        public void Execute()
        {
            _spinWheel.Spin();
        }
    }
}
