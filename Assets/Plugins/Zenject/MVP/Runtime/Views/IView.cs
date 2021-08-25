using System;
using UnityEngine;

namespace Zenject.MVP
{
    public interface IView : IDisposable
    {
        string Name { get; set; }

        Transform Transform { get; }

        bool IsVisible { get; set; }

        IAnimation Show(bool animated = true);

        IAnimation Hide(bool animated = true);
    }

    public interface IView<TPresenter, TView> : IView
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : IView<TPresenter, TView>
    {
    }
}
