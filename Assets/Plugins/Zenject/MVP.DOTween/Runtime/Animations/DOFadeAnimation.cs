using DG.Tweening;
using UnityEngine;

namespace Zenject.MVP.DOTween
{
    public class DOFadeAnimation : DOTweenAnimation
    {
        [Range(0F, 1F)]
        [SerializeField] private float to = 1F;

        public override IAnimation Play()
        {
            if (TryGetComponent<CanvasGroup>(out var canvasGroup))
                tween = canvasGroup.DOFade(to, duration)
                    .SetEase(ease)
                    .SetUpdate(ignoreTimeScale)
                    .OnComplete(OnTweenComplete);
            else OnTweenComplete();

            return this;
        }
    }
}
