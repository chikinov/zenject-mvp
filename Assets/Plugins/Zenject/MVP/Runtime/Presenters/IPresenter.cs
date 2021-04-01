using System;

namespace Zenject.MVP
{
    public interface IPresenter : IDisposable
    {
    }

    public interface IPresenter<TView, TPresenter> : IDisposable
        where TView : IView<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
    {
    }
}
