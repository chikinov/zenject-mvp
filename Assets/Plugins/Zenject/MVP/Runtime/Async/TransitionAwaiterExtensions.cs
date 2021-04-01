namespace Zenject.MVP.Async
{
    public static class TransitionAwaiterExtensions
    {
        public static TransitionAwaiter GetAwaiter(this ITransition transition)
        {
            return new TransitionAwaiter(transition);
        }
    }
}
