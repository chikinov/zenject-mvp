namespace Zenject.MVP
{
    public interface INavigationView : IUIView
    {
        void Push(IUIView view, bool animated = true);

        IUIView Pop(bool animated = true);
    }
}
