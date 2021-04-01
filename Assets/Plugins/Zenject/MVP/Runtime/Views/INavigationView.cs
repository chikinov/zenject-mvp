namespace Zenject.MVP
{
    public interface INavigationView : IUIViewGroup
    {
        void Push(IUIView view, bool animated = true);

        IUIView Pop(bool animated = true);
    }
}
