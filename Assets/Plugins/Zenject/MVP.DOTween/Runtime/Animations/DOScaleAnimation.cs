using DG.Tweening;
using UnityEngine;

namespace Zenject.MVP.DOTween
{
    public class DOScaleAnimation : DOTweenAnimation
    {
        [SerializeField] private Vector3 to = Vector3.one;

        public override IAnimation Play()
        {
            tween = transform.DOScale(to, duration)
                .SetEase(ease)
                .OnComplete(OnTweenComplete);

            return this;
        }
    }
}
