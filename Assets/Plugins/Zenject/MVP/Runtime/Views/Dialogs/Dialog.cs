namespace Zenject.MVP
{
    public class Dialog : UIView
    {
        protected override void Awake()
        {
            base.Awake();

            base.Hide(false);
        }

        public override IAnimation Show(bool animated = true)
        {
            if (Parent != null) Parent.Show(animated);

            return base.Show(animated);
        }

        public override IAnimation Hide(bool animated = true)
        {
            if (Parent != null) Parent.Hide(animated);

            return base.Hide(animated);
        }
    }
}
