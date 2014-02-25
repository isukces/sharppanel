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
    implement Constructor
    implement Constructor *
    implement INotifyPropertyChanged_Passive
    implement ToString ##Domain##
    
    property Domain string A string value that contains the domain name of the addon domain, ie. testop.yourdomain.pl
    
    property Dir string A string value that contains the absolute path to the domain's document root., ie. /home/company_name/public_html/_dirname_
    smartClassEnd
    */

    public partial class RootDomain : NotifyPropertyChangedBase
    {
        #region Static Methods

        // Public Methods 

        public static RootDomain FromXElement(XElement x)
        {
            RootDomain a = new RootDomain();
            a.Fill(x);
            return a;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        /// <summary>
        /// Wyznacza katalog FTP względem katalogu domowego użytkownika
        /// </summary>
        /// <returns></returns>
        public string GetFtpDir()
        {
            if (this is ParkedDomain)
                return "/" + (this as ParkedDomain).Basedir;
            return "/public_html";
        }

        public bool NameEquals(string x)
        {
            return this.domain.ToLower() == x.ToLower();
        }
        // Protected Methods 

        virtual protected void Fill(XElement x)
        {
            Dir = x.Element("docroot").Value;
            Domain = x.Element("domain").Value;
        }

        #endregion Methods

        #region Properties

        public DomainType DomainType
        {
            get
            {
                Type myType = GetType();
                if (myType == typeof(ParkedDomain))
                    return DomainType.Parked;
                if (myType == typeof(AddonDomain))
                    return DomainType.Addon;
                if (myType == typeof(SubDomain))
                    return DomainType.Subdomain;
                if (myType == typeof(RootDomain))
                    return DomainType.Root;
                throw new ArgumentException();
            }
        }

        #endregion Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-22 17:23
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class RootDomain
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public RootDomain()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Domain## ##Dir##
        implement ToString Domain=##Domain##, Dir=##Dir##
        implement equals Domain, Dir
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public RootDomain()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Domain">A string value that contains the domain name of the addon domain, ie. testop.yourdomain.com</param>
        /// <param name="Dir">A string value that contains the absolute path to the domain's document root., ie. /home/company_name/public_html/_dirname_</param>
        /// </summary>
        public RootDomain(string Domain, string Dir)
        {
            this.Domain = Domain;
            this.Dir = Dir;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Domain; A string value that contains the domain name of the addon domain, ie. testop.yourdomain.com
        /// </summary>
        public const string PROPERTYNAME_DOMAIN = "Domain";
        /// <summary>
        /// Nazwa własności Dir; A string value that contains the absolute path to the domain's document root., ie. /home/company_name/public_html/_dirname_
        /// </summary>
        public const string PROPERTYNAME_DIR = "Dir";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", domain);
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// A string value that contains the domain name of the addon domain, ie. testop.yourdomain.com
        /// </summary>
        public string Domain
        {
            get
            {
                return domain;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == domain) return;
                domain = value;
                NotifyPropertyChanged(PROPERTYNAME_DOMAIN);
            }
        }
        private string domain = string.Empty;
        /// <summary>
        /// A string value that contains the absolute path to the domain's document root., ie. /home/company_name/public_html/_dirname_
        /// </summary>
        public string Dir
        {
            get
            {
                return dir;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == dir) return;
                dir = value;
                NotifyPropertyChanged(PROPERTYNAME_DIR);
            }
        }
        private string dir = string.Empty;
        #endregion Properties

    }
}
