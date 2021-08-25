using System;

namespace Zenject.MVP
{
    public class UIDataButtonListView<TData, TDataView> :
        UIDataListView<TData, TDataView>,
        IUIDataButtonListView<TData, TDataView>
        where TDataView : UIDataButtonView<TData>
    {
        public event EventHandler<UIDataView<TData>.EventArgs> OnClick;

        public override TDataView Add(TData data)
        {
            var view = base.Add(data);
            view.OnClick += OnClickView;
            return view;
        }

        public override void Clear()
        {
            foreach (var child in children)
                (child as TDataView).OnClick -= OnClickView;

            base.Clear();
        }

        private void OnClickView(
            object sender, UIDataView<TData>.EventArgs e) =>
            OnClick?.Invoke(sender, e);
    }
}
