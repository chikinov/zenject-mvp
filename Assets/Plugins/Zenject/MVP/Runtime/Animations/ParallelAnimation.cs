using UnityEngine;

namespace Zenject.MVP
{
    public class ParallelAnimation : Animation
    {
        [SerializeField] private Animation[] animations;

        public override bool IsDone
        {
            get
            {
                foreach (var animation in animations)
                {
                    if (animation.IsDone) continue;

                    return false;
                }

                return true;
            }
        }

        public override void Complete()
        {
            foreach (var animation in animations)
            {
                animation.Complete();
            }
        }

        public override IAnimation Play()
        {
            var completeCount = 0;

            foreach (var animation in animations)
            {
                animation.Play().OnComplete(
                    () =>
                    {
                        if (++completeCount < animations.Length) return;

                        onComplete?.Invoke();
                        onComplete = null;
                    });
            }

            return this;
        }

        public override void Stop()
        {
            foreach (var animation in animations)
            {
                animation.Stop();
            }
        }
    }
}
