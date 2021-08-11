namespace Zenject.MVP
{
    public class PoolableDialog : Dialog, IPoolable<IMemoryPool>
    {
        private IMemoryPool pool;

        public virtual void OnSpawned(IMemoryPool pool)
        {
            this.pool = pool;
        }

        public virtual void OnDespawned()
        {
            pool = null;
        }

        public override IAnimation Hide(bool animated = true) =>
            base.Hide(animated).OnComplete(() => pool?.Despawn(this));
    }
}
