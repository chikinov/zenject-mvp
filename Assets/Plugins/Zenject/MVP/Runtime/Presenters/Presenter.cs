using System;

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
            view.OnEnable += HandleViewOnEnable;
            view.OnDisable += HandleViewOnDisable;
        }

        protected virtual void HandleViewOnEnable(
            object sender, EventArgs eventArgs)
        {
        }

        protected virtual void HandleViewOnDisable(
            object sender, EventArgs eventArgs)
        {
        }

        public virtual void Dispose()
        {
            view.OnEnable -= HandleViewOnEnable;
            view.OnDisable -= HandleViewOnDisable;
        }
    }
}
