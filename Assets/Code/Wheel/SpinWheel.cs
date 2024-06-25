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
        public float MinSpeed { get; private set; }
        public GameObject WheelSectors => _wheelSectors;

        [SerializeField] private GameObject _rotationZone;
        [SerializeField] private GameObject _wheelSectors;

        public void Initialize(float initialRotationSpeed, float decelerationSpeed, float stopSpeed)
        {
            InitialRotationSpeed = initialRotationSpeed;
            DecelerationSpeed = decelerationSpeed;
            MinSpeed = stopSpeed;
        }

        public void Spin()
        {
            float currentRotationSpeed = InitialRotationSpeed;
            OnSpinStarted?.Invoke();
            StartCoroutine(StartSpin(currentRotationSpeed));
        }

        private IEnumerator StartSpin(float currentRotationSpeed)
        {
            while (IsRotating(currentRotationSpeed, 100f, 170f))
            {
                currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, MinSpeed, 0.1f * Mathf.Sqrt(DecelerationSpeed * Time.deltaTime * Time.deltaTime));
                currentRotationSpeed = Mathf.Max(currentRotationSpeed, MinSpeed);

                _rotationZone.transform.rotation *= Quaternion.Euler(0, 0, currentRotationSpeed * Time.deltaTime);

                yield return null;
            }

            OnSpinEnded?.Invoke();
        }

        private bool IsRotating(float currentRotationSpeed, float minAngle, float maxAngle)
        {
            float currentAngle = _rotationZone.transform.eulerAngles.z;

            bool notInRange = currentAngle <= minAngle || currentAngle >= maxAngle;
            bool notMinSpeed = Mathf.FloorToInt(currentRotationSpeed) != (int)MinSpeed;

            return notInRange || notMinSpeed;
        }
    }
}
