using System.Collections.Generic;
using UnityEngine;

namespace Zenject.MVP
{
    public abstract class UIViewGroup<TPresenter, TView>
        : UIView<TPresenter, TView>, IUIViewGroup<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : UIViewGroup<TPresenter, TView>
    {
        [SerializeField] private RectTransform childViewParent;

        protected readonly List<IUIView> views = new List<IUIView>();

        public virtual void AddView(IUIView view)
        {
            if (views.Contains(view)) return;

            views.Add(view);

            var rectTransform = view.RectTransform;
            rectTransform.SetParent(
                childViewParent == null ? Transform : childViewParent, true);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.pivot = new Vector2(0.5F, 0.5F);
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
        }

        public virtual bool RemoveView(IUIView view)
        {
            view.Transform.SetParent(null);
            return views.Remove(view);
        }

        public virtual IUIView FindView<T>() where T : IUIView
        {
            return views.Find(view => view is T);
        }
    }
}
