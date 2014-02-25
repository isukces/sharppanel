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
    
    property DomainKey string klucz domeny, np. somedomain.yourprivider.com
    
    property RootDomain string A string value that contains the main domain of the account to which the addon domain belongs, ie. you.yourprovider.com
    
    property SubDomainName string subdomena, np. testop
    smartClassEnd
    */

    public partial class SubDomain : ParkedDomain
    {
        #region Static Methods

        // Public Methods 

        public new static SubDomain FromXElement(XElement x)
        {
            SubDomain a = new SubDomain();
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
            return string.Format("sub-domain {0} at {1}", Domain, Dir);
        }
        // Protected Methods 

        protected override void Fill(XElement x)
        {
            base.Fill(x);
            DomainKey = x.Element("domainkey").Value;
            RootDomain = x.Element("rootdomain").Value;
            SubDomainName = x.Element("subdomain").Value;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-07 18:02
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class SubDomain
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public SubDomain()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##DomainKey## ##RootDomain## ##SubDomainName##
        implement ToString DomainKey=##DomainKey##, RootDomain=##RootDomain##, SubDomainName=##SubDomainName##
        implement equals DomainKey, RootDomain, SubDomainName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności DomainKey; klucz domeny, np. somedomain.yourprivider.com
        /// </summary>
        public const string PROPERTYNAME_DOMAINKEY = "DomainKey";
        /// <summary>
        /// Nazwa własności RootDomain; A string value that contains the main domain of the account to which the addon domain belongs, ie. you.yourprovider.com
        /// </summary>
        public const string PROPERTYNAME_ROOTDOMAIN = "RootDomain";
        /// <summary>
        /// Nazwa własności SubDomainName; subdomena, np. testop
        /// </summary>
        public const string PROPERTYNAME_SUBDOMAINNAME = "SubDomainName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// klucz domeny, np. somedomain.yourprivider.com
        /// </summary>
        public string DomainKey
        {
            get
            {
                return domainKey;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == domainKey) return;
                domainKey = value;
                NotifyPropertyChanged(PROPERTYNAME_DOMAINKEY);
            }
        }
        private string domainKey = string.Empty;
        /// <summary>
        /// A string value that contains the main domain of the account to which the addon domain belongs, ie. you.yourprovider.com
        /// </summary>
        public string RootDomain
        {
            get
            {
                return rootDomain;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == rootDomain) return;
                rootDomain = value;
                NotifyPropertyChanged(PROPERTYNAME_ROOTDOMAIN);
            }
        }
        private string rootDomain = string.Empty;
        /// <summary>
        /// subdomena, np. testop
        /// </summary>
        public string SubDomainName
        {
            get
            {
                return subDomainName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == subDomainName) return;
                subDomainName = value;
                NotifyPropertyChanged(PROPERTYNAME_SUBDOMAINNAME);
            }
        }
        private string subDomainName = string.Empty;
        #endregion Properties

    }
}
