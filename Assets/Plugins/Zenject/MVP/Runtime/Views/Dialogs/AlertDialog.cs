using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject.MVP.Async;

namespace Zenject.MVP
{
    public class AlertDialog : PoolableDialog,
        IPoolable<string, string, string, string, IMemoryPool>
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button positiveButton;
        [SerializeField] private Button negativeButton;
        [SerializeField] private Transform buttonGroup;

        public void OnSpawned(
            string title,
            string message,
            string positiveText,
            string negativeText,
            IMemoryPool pool)
        {
            base.OnSpawned(pool);

            if (titleText) titleText.text = title;

            if (messageText) messageText.text = message;

            if (positiveButton)
            {
                if (string.IsNullOrEmpty(positiveText))
                    positiveButton.gameObject.SetActive(false);
                else
                {
                    var text = positiveButton
                        .GetComponentInChildren<TMP_Text>();
                    if (text) text.SetText(positiveText);

                    positiveButton.gameObject.SetActive(true);
                }
            }

            if (negativeButton)
            {
                if (string.IsNullOrEmpty(negativeText))
                    negativeButton.gameObject.SetActive(false);
                else
                {
                    var text = negativeButton
                        .GetComponentInChildren<TMP_Text>();
                    if (text) text.SetText(negativeText);

                    negativeButton.gameObject.SetActive(true);
                }
            }

            if (buttonGroup) buttonGroup.gameObject.SetActive(
                !string.IsNullOrEmpty(positiveText) ||
                !string.IsNullOrEmpty(negativeText));
        }

        public async Task<bool> ShowAndWaitAsync()
        {
            await Show();

            try
            {
                if (!buttonGroup || !buttonGroup.gameObject.activeSelf ||
                    !positiveButton || !negativeButton)
                    return false;

                var tcs = new TaskCompletionSource<bool>();

                var positiveAction = new UnityAction(
                    () => tcs.TrySetResult(true));
                var negativeAction = new UnityAction(
                    () => tcs.TrySetResult(false));

                positiveButton.onClick.AddListener(positiveAction);
                negativeButton.onClick.AddListener(negativeAction);

                var result = await tcs.Task;

                positiveButton.onClick.RemoveListener(positiveAction);
                negativeButton.onClick.RemoveListener(negativeAction);

                return result;
            }
            finally
            {
                _ = Hide();
            }
        }
    }
}
