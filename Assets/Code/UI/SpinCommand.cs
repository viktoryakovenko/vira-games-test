using UnityEngine;

namespace Code.UI
{
    [RequireComponent(typeof(ButtonHandler))]
    public class SpinCommand : MonoBehaviour, ICommand
    {
        [SerializeField] private SpinWheel _spinWheel;
        private ButtonHandler _buttonHandler;

        private void Awake()
        {
            _buttonHandler = GetComponent<ButtonHandler>();
        }

        private void OnEnable()
        {
            _spinWheel.OnSpinStarted += _buttonHandler.DisableButton;
            _spinWheel.OnSpinEnded += _buttonHandler.EnableButton;
        }

        private void OnDisable()
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
