using Code.Wheel;
using UnityEngine;

namespace Code.UI
{
    public class HUDHandler : MonoBehaviour
    {
        [SerializeField] private SpinCommand _spinCommand;
        [SerializeField] private Counter _counter;

        public SpinCommand SpinCommand => _spinCommand;
        public Counter Counter => _counter;
    }
}
