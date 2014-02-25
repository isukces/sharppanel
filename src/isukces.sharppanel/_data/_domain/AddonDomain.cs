using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    implement INotifyPropertyChanged_Passive
    
    property FullSubdomain DomainName pełna nazwa poddomeny
    smartClassEnd
    */
 

    public partial class AddonDomain : SubDomain
    {
        #region Static Methods

        // Public Methods 

        public new static AddonDomain FromXElement(XElement x)
        {
            AddonDomain a = new AddonDomain();
            a.Fill(x);
            return a;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("addon domain {0} at {1}", Domain, Dir);
        }
        // Protected Methods 

        protected override void Fill(XElement x)
        {
            base.Fill(x);
            FullSubdomain = x.Element("fullsubdomain").Value;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-09 10:33
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class AddonDomain
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AddonDomain()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##FullSubdomain##
        implement ToString FullSubdomain=##FullSubdomain##
        implement equals FullSubdomain
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności FullSubdomain; pełna nazwa poddomeny
        /// </summary>
        public const string PROPERTYNAME_FULLSUBDOMAIN = "FullSubdomain";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// pełna nazwa poddomeny
        /// </summary>
        public DomainName FullSubdomain
        {
            get
            {
                return fullSubdomain;
            }
            set
            {
                if (value == fullSubdomain) return;
                fullSubdomain = value;
                NotifyPropertyChanged(PROPERTYNAME_FULLSUBDOMAIN);
            }
        }
        private DomainName fullSubdomain;
        #endregion Properties

    }
}
