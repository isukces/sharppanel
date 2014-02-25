using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    public class Cache : Dictionary<string, object>
    {
        #region Methods

        // Public Methods 

        public CacheItem<T> GetFromCache<T>(string name)
        {
            object t;
            if (TryGetValue(name, out t))
                return t as CacheItem<T>;
            return null;
        }

        public void Update<T>(string name, T value)
        {
            object t;
            if (TryGetValue(name, out t))
                (t as CacheItem<T>).Update(value);
            else
                this[name] = new CacheItem<T>(value);
        }

        #endregion Methods
    }
}
