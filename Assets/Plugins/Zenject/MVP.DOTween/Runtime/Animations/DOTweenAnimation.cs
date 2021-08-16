using DG.Tweening;
using UnityEngine;

namespace Zenject.MVP.DOTween
{
    public abstract class DOTweenAnimation : Animation
    {
        [SerializeField] protected float duration = 1F;
        [SerializeField] protected Ease ease = Ease.OutQuad;
        [SerializeField] protected bool ignoreTimeScale;

        protected Tween tween;

        public override bool IsDone => tween == null || tween.IsComplete();

        public override void Complete()
        {
            tween?.Complete();
            tween = null;
        }

        protected virtual void OnTweenComplete()
        {
            tween = null;

            onComplete?.Invoke();
            onComplete = null;
        }

        public override void Stop()
        {
            tween?.Kill();
            tween = null;
        }
    }
}
