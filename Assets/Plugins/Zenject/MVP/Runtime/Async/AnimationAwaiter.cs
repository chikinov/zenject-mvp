using System;
using System.Runtime.CompilerServices;

namespace Zenject.MVP.Async
{
    public struct AnimationAwaiter : ICriticalNotifyCompletion
    {
        private readonly IAnimation animation;

        public bool IsCompleted => animation == null || animation.IsDone;

        public void GetResult() { }

        public AnimationAwaiter(IAnimation animation)
        {
            this.animation = animation;
        }

        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            animation?.OnComplete(continuation);
        }
    }
}
