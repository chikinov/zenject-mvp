using System.Collections;
using UnityEngine;

namespace Zenject.MVP
{
    public class FadeAnimation : UIAnimation
    {
        [Range(0F, 1F)]
        [SerializeField] private float from = 0;

        [Range(0F, 1F)]
        [SerializeField] private float to = 1;

        [SerializeField] private float duration = 1;

        private Coroutine coroutine;

        public override bool IsDone => coroutine == null;

        public override IAnimation Play()
        {
            Stop();

            coroutine = StartCoroutine(DoPlay());

            return this;
        }

        public override void Stop()
        {
            if (coroutine == null) return;

            StopCoroutine(coroutine);

            coroutine = null;

            onComplete = null;
        }

        private IEnumerator DoPlay()
        {
            try
            {
                if (!TryGetComponent<IUIView>(out var view)) yield break;

                var t = 0F;
                float alpha;

                do
                {
                    t += Time.deltaTime / duration;
                    alpha = Mathf.Lerp(from, to, t);
                    view.Alpha = alpha;
                    if (from < to ? alpha < to : alpha > to) yield return null;
                    else yield break;
                } while (true);
            }
            finally
            {
                onComplete?.Invoke();
                onComplete = null;
            }
        }

        private void OnDisable()
        {
            Stop();
        }
    }
}
