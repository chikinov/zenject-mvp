namespace Zenject.MVP
{
    public interface IDialogFactory : IFactory
    {
        IUIView ParentView { get; }
    }

    public interface IDialogFactory<
        TParam1, TParam2, TParam3, TParam4, TDialog> : IDialogFactory,
        IFactory<TParam1, TParam2, TParam3, TParam4, TDialog>
    {
    }
}
