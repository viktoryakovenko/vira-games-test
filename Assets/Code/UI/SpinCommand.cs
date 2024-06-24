using UnityEngine;

namespace Code.UI
{
    public class SpinCommand : MonoBehaviour, ICommand
    {
        private ISpinWheel _spinWheel;

        public void Execute()
        {
            _spinWheel.Spin();
        }
    }
}
