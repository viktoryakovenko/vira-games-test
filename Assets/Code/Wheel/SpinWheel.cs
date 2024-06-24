using System;
using System.Collections;
using UnityEngine;

namespace Code.Wheel
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

        public void Initialize(float initialRotationSpeed, float decelerationSpeed)
        {
            InitialRotationSpeed = initialRotationSpeed;
            DecelerationSpeed = decelerationSpeed;
        }

        public void Spin()
        {
            float currentRotationSpeed = InitialRotationSpeed;
            OnSpinStarted?.Invoke();
            StartCoroutine(StartSpin(currentRotationSpeed));
        }

        private IEnumerator StartSpin(float currentRotationSpeed)
        {
            while (currentRotationSpeed > 0)
            {
                currentRotationSpeed -= DecelerationSpeed * Time.deltaTime;
                currentRotationSpeed = Mathf.Max(currentRotationSpeed, 0);

                _rotationZone.transform.rotation *= Quaternion.Euler(0, 0, currentRotationSpeed * Time.deltaTime);

                yield return null;
            }

            OnSpinEnded?.Invoke();
        }
    }
}
