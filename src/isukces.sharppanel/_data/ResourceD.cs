using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor
    implement Constructor count, limit
    implement INotifyPropertyChanged_Passive
    
    property Count double Ilość
    
    property Limit double limit
    
    property IsUnlimited bool czy zasób jest nielimitowany?
    
    property PercentUsed double użycie zasobu w procentach
    	read only isUnlimited ? 0 : limit < 1 ? 100 : 100.0 * count / limit
    	depends on Count
    	depends on Limit
    	depends on IsUnlimited
    smartClassEnd
    */

    public partial class ResourceD : NotifyPropertyChangedBase
    {
        public override string ToString()
        {
            if (isUnlimited)
                return string.Format("{0} of unlimited", count);
            return string.Format("{0} of {1} {2:0.0}%", count, limit, PercentUsed);
        }

        public static ResourceD operator *(ResourceD a, double b)
        {
            if (a.IsUnlimited)
                return new ResourceD(a.count * b, 0) { IsUnlimited = true };
            return new ResourceD(a.count * b, a.limit * b);
        }
        public static ResourceD operator /(ResourceD a, double b)
        {
            if (a.IsUnlimited)
                return new ResourceD(a.count / b, 0) { IsUnlimited = true };
            return new ResourceD(a.count / b, a.limit / b);
        }

        public static ResourceD GetUnlimited(double count)
        {
            return new ResourceD(count, 0) { IsUnlimited = true };
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-10 09:17
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class ResourceD
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ResourceD()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Count## ##Limit## ##IsUnlimited## ##PercentUsed##
        implement ToString Count=##Count##, Limit=##Limit##, IsUnlimited=##IsUnlimited##, PercentUsed=##PercentUsed##
        implement equals Count, Limit, IsUnlimited, PercentUsed
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ResourceD()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Count">Ilość</param>
        /// <param name="Limit">limit</param>
        /// </summary>
        public ResourceD(double Count, double Limit)
        {
            this.Count = Count;
            this.Limit = Limit;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Count; Ilość
        /// </summary>
        public const string PROPERTYNAME_COUNT = "Count";
        /// <summary>
        /// Nazwa własności Limit; limit
        /// </summary>
        public const string PROPERTYNAME_LIMIT = "Limit";
        /// <summary>
        /// Nazwa własności IsUnlimited; czy zasób jest nielimitowany?
        /// </summary>
        public const string PROPERTYNAME_ISUNLIMITED = "IsUnlimited";
        /// <summary>
        /// Nazwa własności PercentUsed; użycie zasobu w procentach
        /// </summary>
        public const string PROPERTYNAME_PERCENTUSED = "PercentUsed";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Ilość
        /// </summary>
        public double Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value == count) return;
                count = value;
                NotifyPropertyChanged(PROPERTYNAME_COUNT);
                NotifyPropertyChanged(PROPERTYNAME_PERCENTUSED);
            }
        }
        private double count;
        /// <summary>
        /// limit
        /// </summary>
        public double Limit
        {
            get
            {
                return limit;
            }
            set
            {
                if (value == limit) return;
                limit = value;
                NotifyPropertyChanged(PROPERTYNAME_LIMIT);
                NotifyPropertyChanged(PROPERTYNAME_PERCENTUSED);
            }
        }
        private double limit;
        /// <summary>
        /// czy zasób jest nielimitowany?
        /// </summary>
        public bool IsUnlimited
        {
            get
            {
                return isUnlimited;
            }
            set
            {
                if (value == isUnlimited) return;
                isUnlimited = value;
                NotifyPropertyChanged(PROPERTYNAME_ISUNLIMITED);
                NotifyPropertyChanged(PROPERTYNAME_PERCENTUSED);
            }
        }
        private bool isUnlimited;
        /// <summary>
        /// użycie zasobu w procentach; własność jest tylko do odczytu.
        /// </summary>
        public double PercentUsed
        {
            get
            {
                return isUnlimited ? 0 : limit < 1 ? 100 : 100.0 * count / limit;
            }
        }
        #endregion Properties

    }
}
