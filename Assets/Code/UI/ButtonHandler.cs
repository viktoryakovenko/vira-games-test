using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    [RequireComponent(typeof(ICommand))]
    public class ButtonHandler : MonoBehaviour
    {
        private ICommand _command;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _command = GetComponent<ICommand>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ExecuteCommand);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ExecuteCommand);
        }

        private void ExecuteCommand()
        {
            _command.Execute();
        }
    }
}