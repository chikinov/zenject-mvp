using System;
using UnityEngine;

namespace Zenject.MVP
{
    public interface IView
    {
        string Name { get; set; }

        Transform Parent { get; }

        Transform Transform { get; }

        bool IsVisible { get; set; }

        ITransition Show(bool animated);

        ITransition Hide(bool animated);

        event EventHandler OnEnable;
        event EventHandler OnDisable;
    }

    public interface IView<TPresenter, TView> : IView
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : IView<TPresenter, TView>
    {
    }
}
