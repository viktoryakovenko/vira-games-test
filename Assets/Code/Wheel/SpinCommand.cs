using Code.UI;
using UnityEngine;

namespace Code.Wheel
{
    [RequireComponent(typeof(ButtonHandler))]
    public class SpinCommand : MonoBehaviour, ICommand
    {
        private ButtonHandler _buttonHandler;
        private ISpinWheel _spinWheel;
        private ISpinChecker _spinChecker;

        public void Initialize(ISpinWheel wheel, ISpinChecker checker)
        {
            _spinWheel = wheel;
            _spinChecker = checker;
            _buttonHandler = GetComponent<ButtonHandler>();
            _spinWheel.OnSpinStarted += _buttonHandler.DisableButton;
            _spinWheel.OnSpinEnded += CheckSpinCounts;
        }

        private void OnDestroy()
        {
            _spinWheel.OnSpinStarted -= _buttonHandler.DisableButton;
            _spinWheel.OnSpinEnded -= CheckSpinCounts;
        }

        public void Execute()
        {
            _spinWheel.Spin();
        }

        private void CheckSpinCounts()
        {
            if (_spinChecker.CanSpin)
                _buttonHandler.EnableButton();
        }
    }
}
