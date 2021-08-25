namespace Zenject.MVP
{
    public abstract class UIDataView<TData> : UIView, IDataView<TData>
    {
        private TData data;
        public virtual TData Data
        {
            get => data;
            set
            {
                data = value;

                Refresh();
            }
        }

        public abstract void Refresh();

        public class EventArgs : System.EventArgs
        {
            public TData Data { get; private set; }

            internal EventArgs(TData data)
            {
                Data = data;
            }
        }
    }
}
