using System;
using UnityEngine;

namespace Zenject.MVP
{
    public abstract class UIAnimation : MonoBehaviour, ITransition
    {
        protected Action onComplete;

        public abstract bool IsDone { get; }

        public abstract ITransition Play();

        public abstract void Stop();

        public virtual ITransition OnComplete(Action callback)
        {
            if (IsDone)
            {
                callback?.Invoke();
                return this;
            }

            onComplete += callback;
            return this;
        }
    }
}
