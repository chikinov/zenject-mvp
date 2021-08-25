using System;

namespace Zenject.MVP
{
    public interface IUIDataButtonView<TData> : IDataView<TData>
    {
        public event EventHandler<UIDataView<TData>.EventArgs> OnClick;
    }
}
