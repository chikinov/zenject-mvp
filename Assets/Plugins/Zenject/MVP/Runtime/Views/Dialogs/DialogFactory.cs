namespace Zenject.MVP
{
    public abstract class DialogFactory<
        TParam1, TParam2, TParam3, TParam4, TPool, TDialog> :
        IDialogFactory<TParam1, TParam2, TParam3, TParam4, TDialog>
        where TPool : DialogPool<TParam1, TParam2, TParam3, TParam4, TDialog>
        where TDialog : PoolableDialog,
        IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool>
    {
        [Inject] private readonly LazyInject<TPool> pool;

        public IUIView ParentView => pool.Value.ParentView;

        public virtual TDialog Create(
            TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) =>
            pool.Value.Spawn(param1, param2, param3, param4, pool.Value);
    }
}
