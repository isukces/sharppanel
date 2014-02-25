using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    public class ObservableHelper<T>
    {
        #region Methods

        // Public Methods 

        public IDisposable Add(IObserver<T> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException("observer");
            }
            lock (thisLock)
            {
                int k = key++;
                subscribers.Add(k, observer);
                return new AnonymousDisposable(() =>
                {
                    lock (thisLock)
                    {
                        subscribers.Remove(k);
                    }
                });
            }
        }

        public void PushOnNext(T args)
        {
            foreach (var i in Observers)
                i.OnNext(args);
        }

        #endregion Methods

        #region Fields

        int key = 0;
        Dictionary<int, IObserver<T>> subscribers = new Dictionary<int, IObserver<T>>();
        private readonly object thisLock = new object();

        #endregion Fields

        public IObserver<T>[] Observers
        {
            get
            {
                lock (thisLock)
                {
                    return subscribers.Values.ToArray();
                }
            }
        }
    }
}


