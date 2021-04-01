using System;
using System.Runtime.CompilerServices;

namespace Zenject.MVP.Async
{
    public struct TransitionAwaiter : ICriticalNotifyCompletion
    {
        private readonly ITransition transition;

        public bool IsCompleted => transition == null || transition.IsDone;

        public void GetResult() { }

        public TransitionAwaiter(ITransition transition)
        {
            this.transition = transition;
        }

        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            transition?.OnComplete(continuation);
        }
    }
}
