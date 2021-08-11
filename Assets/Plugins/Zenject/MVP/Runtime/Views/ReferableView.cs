using UnityEngine;

namespace Zenject.MVP
{
    public class ReferableView : UIView
    {
        private int referenceCount;

        public override IAnimation Show(bool animated = true)
        {
            if (referenceCount++ > 0) return Animation.Placeholder;

            return base.Show(animated);
        }

        public override IAnimation Hide(bool animated = true)
        {
            if ((referenceCount = Mathf.Max(referenceCount - 1, 0)) > 0)
                return Animation.Placeholder;

            return base.Hide(animated);
        }
    }
}
