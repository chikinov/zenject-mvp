using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Zenject.MVP
{
    public class BackHandler : MonoBehaviour
    {
        public delegate bool OnBackDelegate();

        public event OnBackDelegate OnBack;

        private void OnEnable()
        {
            if (EventSystem.current &&
                EventSystem.current.currentInputModule is
                InputSystemUIInputModule module)
                module.cancel.action.performed += OnCancelActionPerformed;
        }

        private void OnDisable()
        {
            if (EventSystem.current &&
                EventSystem.current.currentInputModule is
                InputSystemUIInputModule module)
                module.cancel.action.performed -= OnCancelActionPerformed;
        }

        private void OnCancelActionPerformed(
            InputAction.CallbackContext context)
        {
            if (OnBack == null) return;

            var delegates = OnBack.GetInvocationList();
            for (var i = delegates.Length - 1; i >= 0; i--)
                if ((delegates[i] as OnBackDelegate).Invoke()) return;
        }
    }
}
