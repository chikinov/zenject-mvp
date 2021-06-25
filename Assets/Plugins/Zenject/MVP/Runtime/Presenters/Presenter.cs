namespace Zenject.MVP
{
    public abstract class Presenter<TView, TPresenter>
        : IPresenter<TView, TPresenter>
        where TView : IView<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
    {
        protected readonly TView view;

        [Inject]
        public Presenter(TView view)
        {
            this.view = view;
        }

        public virtual void Initialize()
        {
        }

        public virtual void OnViewShow()
        {
        }

        public virtual void OnViewHide()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}
