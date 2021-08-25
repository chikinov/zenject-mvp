using UnityEngine;

namespace Zenject.MVP
{
    public class UIDataListView<TData, TDataView> :
        UIDataView<TData[]>, IDataListView<TData, TDataView>
        where TDataView : UIDataView<TData>
    {
        [SerializeField] private TDataView dataViewPrefab;
        [SerializeField] private Transform dataViewParent;

        public override Transform ChildParentTransform =>
            dataViewParent ? dataViewParent : dataViewParent = Transform;

        public override TData[] Data
        {
            get
            {
                var block = DisposeBlock.Spawn();

                var list = block.SpawnList<TData>();

                foreach (var child in children)
                {
                    if (!(child is TDataView view)) continue;

                    list.Add(view.Data);
                }

                return list.ToArray();
            }
            set => Populate(value);
        }

        public virtual void Populate(params TData[] dataList)
        {
            foreach (var data in dataList) Add(data);
        }

        public virtual TDataView Add(TData data)
        {
            var view = Instantiate(dataViewPrefab);
            view.Parent = this;
            view.Data = data;
            return view;
        }

        public virtual void Clear()
        {
            for (var i = children.Count - 1; i >= 0; i--)
                children[i].Dispose();
        }

        public override void Refresh()
        {
            foreach (var child in children)
            {
                if (!(child is TDataView view)) continue;

                view.Refresh();
            }
        }
    }
}
