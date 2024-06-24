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
        public float InitialRotationSpeed { get; private set; } = 3600f;
        public float DecelerationSpeed { get; set; } = 720f;

        public void Spin()
        {
            CurrentRotationSpeed = InitialRotationSpeed;
            OnSpinStarted?.Invoke();
            StartCoroutine(StartSpin());
        }

        private IEnumerator StartSpin()
        {
            while (CurrentRotationSpeed > 0)
            {
                CurrentRotationSpeed -= DecelerationSpeed * Time.deltaTime;
                CurrentRotationSpeed = Mathf.Max(CurrentRotationSpeed, 0);

                transform.rotation *= Quaternion.Euler(0, 0, CurrentRotationSpeed * Time.deltaTime);

                yield return null;
            }

            OnSpinEnded?.Invoke();
        }
    }
}
