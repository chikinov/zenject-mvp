using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zenject.MVP
{
    public class UIView : UIBehaviour, IUIView
    {
        [SerializeField] private Animation showAnimation;
        [SerializeField] private Animation hideAnimation;

        protected readonly List<IUIView> children = new List<IUIView>();

        protected virtual Transform ChildParentTransform => Transform;

        public string Name
        {
            get => IsDestroyed() || !gameObject ? null : gameObject.name;
            set
            {
                if (IsDestroyed() || !gameObject) return;

                gameObject.name = value;
            }
        }

        public Transform Transform => RectTransform;

        public virtual bool IsVisible
        {
            get => !IsDestroyed() && gameObject && gameObject.activeSelf;
            set
            {
                if (IsDestroyed() || !gameObject) return;

                gameObject.SetActive(value);
            }
        }

        private IUIView parent;
        public IUIView Parent
        {
            get => parent;
            set
            {
                if (value == parent) return;

                if (parent != null) parent.RemoveChild(this);

                parent = value;

                if (parent == null) return;

                parent.AddChild(this);
            }
        }

        private RectTransform rectTransform;
        public RectTransform RectTransform =>
            IsDestroyed() ? null : rectTransform ?
            rectTransform : rectTransform = transform as RectTransform;

        private CanvasGroup canvasGroup;
        public virtual CanvasGroup CanvasGroup =>
            IsDestroyed() ? null : canvasGroup ?
            canvasGroup : canvasGroup = GetComponent<CanvasGroup>();

        public virtual float Alpha
        {
            get => CanvasGroup ? canvasGroup.alpha : 0F;
            set
            {
                if (CanvasGroup) canvasGroup.alpha = value;
            }
        }

        public bool Interactable
        {
            get => CanvasGroup && canvasGroup.interactable;
            set
            {
                if (CanvasGroup) canvasGroup.interactable = value;
            }
        }

        public virtual IAnimation Show(bool animated = true)
        {
            if (hideAnimation) hideAnimation.Stop();

            IsVisible = true;

            Interactable = true;

            if (showAnimation)
            {
                showAnimation.Play();

                if (!animated) showAnimation.Complete();

                return showAnimation;
            }

            return Animation.Placeholder;
        }

        public virtual IAnimation Hide(bool animated = true)
        {
            if (showAnimation) showAnimation.Stop();

            Interactable = false;

            if (hideAnimation)
            {
                hideAnimation.Play().OnComplete(() => IsVisible = false);

                if (!animated) hideAnimation.Complete();

                return hideAnimation;
            }

            IsVisible = false;

            return Animation.Placeholder;
        }

        void IUIView.AddChild(IUIView view)
        {
            if (children.Contains(view)) return;

            children.Add(view);

            var rectTransform = view.RectTransform;
            rectTransform.SetParent(ChildParentTransform);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.pivot = new Vector2(0.5F, 0.5F);
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
        }

        void IUIView.RemoveChild(IUIView view)
        {
            if (children.Remove(view)) view.Transform.SetParent(null);
        }

        public virtual IUIView FindChild<T>() where T : IUIView
        {
            return children.Find(view => view is T);
        }
    }

    public class UIView<TPresenter, TView>
        : UIView, IUIView<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : UIView<TPresenter, TView>
    {
        protected TPresenter presenter;

        protected override void Awake()
        {
            base.Awake();

            base.Hide(false);
        }

        [Inject]
        public virtual void Construct(TPresenter presenter)
        {
            this.presenter = presenter;
            presenter.Initialize();
        }

        public override IAnimation Show(bool animated = true)
        {
            var animation = base.Show(animated);

            presenter.OnViewShow();

            return animation;
        }

        public override IAnimation Hide(bool animated = true)
        {
            presenter.OnViewHide();

            return base.Hide(animated);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            presenter.Dispose();
        }
    }
}
