using System;

namespace Zenject.MVP
{
    public sealed class PlaceholderAnimation : IAnimation
    {
        public bool IsDone { get; } = true;

        public IAnimation Play() => this;

        public void Stop() { }

        public void Complete() { }

        public IAnimation OnComplete(Action callback)
        {
            callback?.Invoke();

            return this;
        }
    }
}
