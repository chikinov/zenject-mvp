using System;

namespace Zenject.MVP
{
    public interface ITransition
    {
        bool IsDone { get; }

        ITransition Run();

        void Stop();

        ITransition OnComplete(Action callback);
    }
}
