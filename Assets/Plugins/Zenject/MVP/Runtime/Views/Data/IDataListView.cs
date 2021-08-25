namespace Zenject.MVP
{
    public interface IDataListView<TData, TDataView> : IDataView<TData[]>
        where TDataView : IDataView<TData>
    {
        void Populate(params TData[] dataList);

        TDataView Add(TData data);

        void Clear();
    }
}
