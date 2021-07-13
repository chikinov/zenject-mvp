using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zenject.MVP
{
    public abstract class UIView : UIBehaviour, IUIView
    {
        [SerializeField] private UIAnimation showAnimation;
        [SerializeField] private UIAnimation hideAnimation;

        public string Name
        {
            get => IsDestroyed() || !gameObject ? null : gameObject.name;
            set
            {
                if (IsDestroyed() || !gameObject) return;

                gameObject.name = value;
            }
        }

        public Transform Parent => RectTransform ? rectTransform.parent : null;

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

        private RectTransform rectTransform;
        public RectTransform RectTransform =>
            IsDestroyed() ? null : rectTransform ??= transform as RectTransform;

        private CanvasGroup canvasGroup;
        public virtual CanvasGroup CanvasGroup =>
            IsDestroyed() ? null : canvasGroup ??= GetComponent<CanvasGroup>();

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

        public virtual ITransition Show(bool animated = true)
        {
            if (hideAnimation) hideAnimation.Stop();

            IsVisible = true;

            Interactable = true;

            return new Animation(showAnimation, animated).Play();
        }

        public virtual ITransition Hide(bool animated = true)
        {
            if (showAnimation) showAnimation.Stop();

            Interactable = false;

            return new Animation(hideAnimation, animated).Play()
                .OnComplete(() => IsVisible = false);
        }

        private struct Animation : ITransition
        {
            private readonly UIAnimation animation;
            private readonly bool animated;

            public bool IsDone =>
                !animated || animation == null || animation.IsDone;

            public Animation(UIAnimation animation, bool animated)
            {
                this.animation = animation;
                this.animated = animated;
            }

            public ITransition Play()
            {
                if (animated && animation) animation.Play();
                return this;
            }

            public void Stop()
            {
                animation.Stop();
            }

            public ITransition OnComplete(Action callback)
            {
                if (animated && animation) animation.OnComplete(callback);
                else callback?.Invoke();
                return this;
            }
        }
    }

    public abstract class UIView<TPresenter, TView>
        : UIView, IUIView<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : UIView<TPresenter, TView>
    {
        protected TPresenter presenter;

        protected override void Awake()
        {
            base.Awake();

            Interactable = false;
            IsVisible = false;
        }

        [Inject]
        public virtual void Construct(TPresenter presenter)
        {
            this.presenter = presenter;
            presenter.Initialize();
        }

        public override ITransition Show(bool animated = true)
        {
            var transition = base.Show(animated);

            presenter.OnViewShow();

            return transition;
        }

        public override ITransition Hide(bool animated = true)
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
