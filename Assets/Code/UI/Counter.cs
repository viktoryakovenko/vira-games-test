using Code.Wheel;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;

        private ISpinChecker _spinChecker;
        private int _totalSpins;

        public void Initialize(ISpinChecker spinChecker)
        {
            _spinChecker = spinChecker;
            _spinChecker.OnCountChanged += UpdateCounter;
            UpdateCounter();
        }

        private void OnDestroy()
        {
            if (_spinChecker != null)
            {
                _spinChecker.OnCountChanged -= UpdateCounter;
            }
        }

        private void UpdateCounter()
        {
            _counterText.text = $"Spins: {_spinChecker.TotalSpins}";
        }
    }
}
