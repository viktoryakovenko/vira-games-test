using System;
using System.Collections;
using UnityEngine;

namespace Code.UI
{
    public class SpinWheel : MonoBehaviour, ISpinWheel
    {
        public event Action OnSpinStarted;
        public event Action OnSpinEnded;

        public float InitialRotationSpeed { get; private set; }
        public float DecelerationSpeed { get; private set; }
        public GameObject WheelSectors => _wheelSectors;

        [SerializeField] private GameObject _rotationZone;
        [SerializeField] private GameObject _wheelSectors;

        private float _currentRotationSpeed;

        public void Initialize(float initialRotationSpeed, float decelerationSpeed)
        {
            InitialRotationSpeed = initialRotationSpeed;
            DecelerationSpeed = decelerationSpeed;
        }

        public void Spin()
        {
            _currentRotationSpeed = InitialRotationSpeed;
            OnSpinStarted?.Invoke();
            StartCoroutine(StartSpin());
        }

        private IEnumerator StartSpin()
        {
            while (_currentRotationSpeed > 0)
            {
                _currentRotationSpeed -= DecelerationSpeed * Time.deltaTime;
                _currentRotationSpeed = Mathf.Max(_currentRotationSpeed, 0);

                _rotationZone.transform.rotation *= Quaternion.Euler(0, 0, _currentRotationSpeed * Time.deltaTime);

                yield return null;
            }

            OnSpinEnded?.Invoke();
        }
    }
}
