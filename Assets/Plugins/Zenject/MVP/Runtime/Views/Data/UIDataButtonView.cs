using System;
using UnityEngine;
using UnityEngine.UI;

namespace Zenject.MVP
{
    public abstract class UIDataButtonView<TData> :
        UIDataView<TData>, IUIDataButtonView<TData>
    {
        [SerializeField] private Button button;

        public event EventHandler<EventArgs> OnClick;

        protected override void Awake()
        {
            base.Awake();

            if (!button && !TryGetComponent(out button)) return;

            button.onClick.AddListener(OnClickButton);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (button) button.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton() =>
            OnClick?.Invoke(this, new EventArgs(Data));
    }
}
