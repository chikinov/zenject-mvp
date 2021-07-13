using System;

namespace Zenject.MVP
{
    public sealed class PlaceholderAnimation : ITransition
    {
        public bool IsDone { get; } = true;

        public ITransition Play() => this;

        public void Stop() { }

        public ITransition OnComplete(Action callback)
        {
            callback?.Invoke();

            return this;
        }
    }
}
