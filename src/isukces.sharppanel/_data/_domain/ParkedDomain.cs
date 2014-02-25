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
    
    property Basedir string A string value that contains the relative path to the domain's document root, ie. public_html/_dirname_
    
    property RelDir string ścieżka względna, np. home:public_html/_dirname_
    
    property Status string status, np. not redirected
    smartClassEnd
    */

    public partial class ParkedDomain : RootDomain
    {
        #region Static Methods

        // Public Methods 

        public new static ParkedDomain FromXElement(XElement x)
        {
            ParkedDomain a = new ParkedDomain();
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
            return string.Format("parked domain {0} at {1}", Domain, Dir);
        }
        // Protected Methods 

        protected override void Fill(XElement x)
        {
            // base.Fill(x); - tu są inne nazwy tagów do 
            Basedir = x.Element("basedir").Value;
            Dir = x.Element("dir").Value;
            Domain = x.Element("domain").Value;
            RelDir = x.Element("reldir").Value;
            Status = x.Element("status").Value;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-22 17:31
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class ParkedDomain
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ParkedDomain()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Basedir## ##RelDir## ##Status##
        implement ToString Basedir=##Basedir##, RelDir=##RelDir##, Status=##Status##
        implement equals Basedir, RelDir, Status
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności Basedir; A string value that contains the relative path to the domain's document root, ie. public_html/_dirname_
        /// </summary>
        public const string PROPERTYNAME_BASEDIR = "Basedir";
        /// <summary>
        /// Nazwa własności RelDir; ścieżka względna, np. home:public_html/_dirname_
        /// </summary>
        public const string PROPERTYNAME_RELDIR = "RelDir";
        /// <summary>
        /// Nazwa własności Status; status, np. not redirected
        /// </summary>
        public const string PROPERTYNAME_STATUS = "Status";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// A string value that contains the relative path to the domain's document root, ie. public_html/_dirname_
        /// </summary>
        public string Basedir
        {
            get
            {
                return basedir;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == basedir) return;
                basedir = value;
                NotifyPropertyChanged(PROPERTYNAME_BASEDIR);
            }
        }
        private string basedir = string.Empty;
        /// <summary>
        /// ścieżka względna, np. home:public_html/_dirname_
        /// </summary>
        public string RelDir
        {
            get
            {
                return relDir;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == relDir) return;
                relDir = value;
                NotifyPropertyChanged(PROPERTYNAME_RELDIR);
            }
        }
        private string relDir = string.Empty;
        /// <summary>
        /// status, np. not redirected
        /// </summary>
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == status) return;
                status = value;
                NotifyPropertyChanged(PROPERTYNAME_STATUS);
            }
        }
        private string status = string.Empty;
        #endregion Properties
    }
}
