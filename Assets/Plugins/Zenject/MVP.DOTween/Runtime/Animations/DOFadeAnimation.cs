using DG.Tweening;
using UnityEngine;

namespace Zenject.MVP.DOTween
{
    public class DOFadeAnimation : Animation
    {
        [Range(0F, 1F)]
        [SerializeField] private float to = 1F;

        [SerializeField] private float duration = 1F;

        [SerializeField] private Ease ease = Ease.OutQuad;

        private Tween tween;

        public override bool IsDone => tween == null || tween.IsComplete();

        public override IAnimation Play()
        {
            if (TryGetComponent<CanvasGroup>(out var canvasGroup))
                tween = canvasGroup.DOFade(to, duration)
                    .SetEase(ease)
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
