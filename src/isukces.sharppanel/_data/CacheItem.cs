using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    
    property LoadTime DateTimeOffset czas utworzenia obiektu
    	read only
    
    property Item T obiekt
    	read only
    smartClassEnd
    */

    public partial class CacheItem<T>
    {
        public CacheItem(T item)
        {
            this.loadTime = DateTimeOffset.Now;
            this.item = item;
        }
        public void Update(T item)
        {
            this.loadTime = DateTimeOffset.Now;
            this.item = item;
        }

        public bool IsExpired(double seconds)
        {
            return DateTimeOffset.Now.Subtract(loadTime).TotalSeconds > seconds;
        }


    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-08 16:40
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class CacheItem<T>
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public CacheItem<T>()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##LoadTime## ##Item##
        implement ToString LoadTime=##LoadTime##, Item=##Item##
        implement equals LoadTime, Item
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności LoadTime; czas utworzenia obiektu
        /// </summary>
        public const string PROPERTYNAME_LOADTIME = "LoadTime";
        /// <summary>
        /// Nazwa własności Item; obiekt
        /// </summary>
        public const string PROPERTYNAME_ITEM = "Item";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// czas utworzenia obiektu; własność jest tylko do odczytu.
        /// </summary>
        public DateTimeOffset LoadTime
        {
            get
            {
                return loadTime;
            }
        }
        private DateTimeOffset loadTime;
        /// <summary>
        /// obiekt; własność jest tylko do odczytu.
        /// </summary>
        public T Item
        {
            get
            {
                return item;
            }
        }
        private T item;
        #endregion Properties

    }
}
