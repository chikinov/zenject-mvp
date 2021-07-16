using System;

namespace Zenject.MVP
{
    public interface IAnimation
    {
        bool IsDone { get; }

        IAnimation Play();

        void Stop();

        void Complete();

        IAnimation OnComplete(Action callback);
    }
}
