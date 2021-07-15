namespace Zenject.MVP.Async
{
    public static class AnimationAwaiterExtensions
    {
        public static AnimationAwaiter GetAwaiter(this IAnimation animation)
        {
            return new AnimationAwaiter(animation);
        }
    }
}
