using UnityEngine;

namespace Zenject.MVP
{
    public interface IUIView : IView
    {
        RectTransform RectTransform { get; }

        CanvasGroup CanvasGroup { get; }

        float Alpha { get; set; }

        bool Interactable { get; set; }

        ITransition Show(bool animated = true);
    }

    public interface IUIView<TPresenter, TView>
        : IUIView, IView<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : IUIView<TPresenter, TView>
    {
    }
}
