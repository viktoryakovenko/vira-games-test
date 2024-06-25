using System;
using System.Collections;
using Code.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace Code.Wheel
{
    [RequireComponent(typeof(Win))]
    public class SpinWheel : MonoBehaviour, ISpinWheel
    {
        public event Action OnSpinStarted;
        public event Action OnSpinEnded;

        [SerializeField] private GameObject _rotationZone;

        public float InitialRotationSpeed { get; private set; }
        public int FullTurns { get; private set; }

        private IRandomService _randomService;
        private Win _win;

        public void Initialize(IRandomService randomService, float initialRotationSpeed, int spinsCount)
        {
            InitialRotationSpeed = initialRotationSpeed;
            FullTurns = spinsCount;
            _randomService = randomService;
            _win = GetComponent<Win>();
        }

        public void Spin()
        {
            OnSpinStarted?.Invoke();
            StartCoroutine(StartSpin());
        }

        private IEnumerator StartSpin()
        {
            var id = _win.GetWinItem(out var prize);
            GetMinAndMaxAngles(id, out float prizeMinAngle, out float prizeMaxAngle);

            float currentRotationSpeed = InitialRotationSpeed;

            var slowdownTime = GetSlowdownTime(FullTurns, currentRotationSpeed, prizeMinAngle, prizeMaxAngle);
            float slowdown = currentRotationSpeed / slowdownTime;
            float elapsedTime = 0f;

            while (elapsedTime < slowdownTime)
            {
                currentRotationSpeed -= slowdown * Time.deltaTime;
                _rotationZone.transform.rotation *= Quaternion.Euler(0, 0, currentRotationSpeed * Time.deltaTime);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            OnSpinEnded?.Invoke();
        }

        private void GetMinAndMaxAngles(int id, out float prizeMinAngle, out float prizeMaxAngle)
        {
            var offset = _randomService.Next(5, 10);
            prizeMinAngle = id * 360f / _win.PrizesCount + offset;
            prizeMaxAngle = (id + 1) * 360f / _win.PrizesCount - offset;
        }

        private float GetSlowdownTime(int fullTurns, float currentRotationSpeed, float prizeMinAngle, float prizeMaxAngle)
        {
            float randomAngle = _randomService.Next(prizeMinAngle, prizeMaxAngle);
            float distance = 360f * fullTurns - randomAngle - _rotationZone.transform.eulerAngles.z;
            float slowdownTime = 2 * distance / currentRotationSpeed;
            return slowdownTime;
        }
    }
}
