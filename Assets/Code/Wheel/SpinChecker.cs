using System;
using UnityEngine;

namespace Code.Wheel
{
    [RequireComponent(typeof(ISpinWheel))]
    public class SpinChecker : MonoBehaviour, ISpinChecker
    {
        public event Action OnCountChanged;

        public int TotalSpins => _totalSpins;
        public bool CanSpin => _totalSpins > 0;

        private ISpinWheel _spinWheel;
        private int _totalSpins;

        public void Initialize(ISpinWheel spinWheel, int amount)
        {
            _totalSpins = amount;
            _spinWheel = spinWheel;
            _spinWheel.OnSpinStarted += DecreaseCount;
        }

        private void OnDestroy()
        {
            if (_spinWheel != null)
            {
                _spinWheel.OnSpinStarted -= DecreaseCount;
            }
        }

        private void DecreaseCount()
        {
            _totalSpins--;
            OnCountChanged?.Invoke();
        }
    }
}
