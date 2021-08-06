using System.Collections.Generic;

namespace Zenject.MVP
{
    public abstract class NavigationView<TPresenter, TView>
        : UIView<TPresenter, TView>, INavigationView
        where TPresenter : IPresenter<TView, TPresenter>
        where TView : NavigationView<TPresenter, TView>
    {
        protected readonly Stack<IUIView> stack = new Stack<IUIView>();

        public void Push(IUIView view, bool animated = true)
        {
            if (stack.Count > 0)
            {
                var topView = stack.Peek();
                topView.Hide(animated);
            }

            stack.Push(view);
            view.Show(animated);
        }

        public IUIView Pop(bool animated = true)
        {
            if (stack.Count <= 0) return null;

            var removedView = stack.Pop();
            removedView.Hide(animated);

            if (stack.Count > 0)
            {
                var topView = stack.Peek();
                topView.Show(animated);
            }

            return removedView;
        }
    }
}
