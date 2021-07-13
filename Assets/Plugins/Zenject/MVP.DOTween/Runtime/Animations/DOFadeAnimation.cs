using DG.Tweening;
using UnityEngine;

namespace Zenject.MVP.DOTween
{
    public class DOFadeAnimation : UIAnimation
    {
        [Range(0F, 1F)]
        [SerializeField] private float to = 1F;

        [SerializeField] private float duration = 1F;

        private Tween tween;

        public override bool IsDone => tween == null || tween.IsComplete();

        public override ITransition Play()
        {
            if (TryGetComponent<IUIView>(out var view))
                tween = view.CanvasGroup.DOFade(to, duration)
                    .OnComplete(OnTweenComplete);
            else OnTweenComplete();

            return this;
        }

        private void OnTweenComplete()
        {
            tween = null;

            onComplete?.Invoke();
            onComplete = null;
        }

        public override void Stop()
        {
            tween?.Complete();
        }
    }
}
