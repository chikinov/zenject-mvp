using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zenject.MVP
{
    public abstract class UIView<TPresenter, TView>
        : UIBehaviour, IUIView<TPresenter, TView>
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : UIView<TPresenter, TView>
    {
        [SerializeField] private UIAnimation showAnimation;
        [SerializeField] private UIAnimation hideAnimation;

        protected TPresenter presenter;

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

        [Inject]
        public virtual void Construct(TPresenter presenter)
        {
            this.presenter = presenter;
            presenter.Initialize();
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

        protected override void Awake()
        {
            base.Awake();

            Hide(false);
        }

        private EventHandler onEnable;
        event EventHandler IView.OnEnable
        {
            add => onEnable += value;
            remove => onEnable -= value;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            onEnable?.Invoke(this, EventArgs.Empty);
        }

        private EventHandler onDisable;
        event EventHandler IView.OnDisable
        {
            add => onDisable += value;
            remove => onDisable -= value;
        }

        protected override void OnDisable()
        {
            onDisable?.Invoke(this, EventArgs.Empty);

            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            presenter.Dispose();
        }

        private struct Animation : IAnimation
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

            ITransition ITransition.Run() => Play();

            public IAnimation Play()
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
}
