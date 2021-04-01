namespace Zenject.MVP
{
    public interface IUIViewGroup : IUIView
    {
        void AddView(IUIView view);

        bool RemoveView(IUIView view);

        IUIView FindView<T>() where T : IUIView;
    }

    public interface IUIViewGroup<TPresenter, TView>
        : IUIViewGroup, IUIView<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : IUIViewGroup<TPresenter, TView>
    {
    }
}
