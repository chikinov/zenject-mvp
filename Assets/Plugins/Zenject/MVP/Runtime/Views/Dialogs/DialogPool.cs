namespace Zenject.MVP
{
    public class DialogPool<TParam1, TParam2, TParam3, TParam4, TDialog> :
        MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, IMemoryPool,
            TDialog>, IDialogPool
        where TDialog : PoolableDialog,
        IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool>
    {
        [Inject] private readonly IUIView parentView;
        public IUIView ParentView => parentView;

        protected override void OnCreated(TDialog dialog)
        {
            parentView.Hide(false);

            dialog.Parent = parentView;

            base.OnCreated(dialog);
        }
    }
}
