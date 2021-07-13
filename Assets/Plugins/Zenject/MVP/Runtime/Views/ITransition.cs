using System;

namespace Zenject.MVP
{
    public interface ITransition
    {
        bool IsDone { get; }

        ITransition Play();

        void Stop();

        ITransition OnComplete(Action callback);
    }
}
