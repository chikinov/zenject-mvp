using UnityEngine;

namespace Zenject.MVP
{
    public interface IUIView : IView
    {
        IUIView Parent { get; set; }

        RectTransform RectTransform { get; }

        CanvasGroup CanvasGroup { get; }

        float Alpha { get; set; }

        bool Interactable { get; set; }

        internal void AddChild(IUIView view);

        internal void RemoveChild(IUIView view);

        IUIView FindChild<T>() where T : IUIView;
    }

    public interface IUIView<TPresenter, TView>
        : IUIView, IView<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : IUIView<TPresenter, TView>
    {
    }
}
