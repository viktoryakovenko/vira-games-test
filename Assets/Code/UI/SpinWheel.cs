using System;
using System.Collections;
using UnityEngine;

namespace Code.UI
{
    public class SpinWheel : MonoBehaviour, ISpinWheel
    {
        public event Action OnSpinStarted;
        public event Action OnSpinEnded;

        public float CurrentRotationSpeed { get; set; }
        public float InitialRotationSpeed { get; private set; } = 720f;
        public float DecelerationSpeed { get; set; } = 60f;

        public void Spin()
        {
            CurrentRotationSpeed = InitialRotationSpeed;
            OnSpinStarted?.Invoke();
            StartCoroutine(StartSpin());
        }

        private IEnumerator StartSpin()
        {
            while (CurrentRotationSpeed >= 0)
            {
                CurrentRotationSpeed -= DecelerationSpeed * Time.deltaTime;

                transform.Rotate(Vector3.back, CurrentRotationSpeed * Time.deltaTime);

                yield return null;
            }

            OnSpinEnded?.Invoke();
        }
    }
}
