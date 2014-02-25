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
    implement ToString ##Name## (##Type##) at ##HomeDir##
    
    property Name string Nazwa użytkownika FTP
    
    property Type FtpUserType Typ użytkownika
    
    property HomeDir string katalog domowy
    smartClassEnd
    */

    public partial class FtpUser : NotifyPropertyChangedBase
    {
        #region Constructors

        static FtpUser()
        {

            var a = Enum.GetValues(typeof(FtpUserType)).OfType<FtpUserType>().ToArray();
            AllTypes = string.Join("|", a).ToLower();
        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 

        public static FtpUser FromXElement(XElement x)
        {
            return new FtpUser()
            {
                HomeDir = x.Element("homedir").Value,
                Name = x.Element("user").Value,
                Type = (FtpUserType)Enum.Parse(typeof(FtpUserType), x.Element("type").Value, true)
            };
        }

        #endregion Static Methods

        #region Static Fields

        public static readonly string AllTypes;

        #endregion Static Fields
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-08 19:09
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class FtpUser
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public FtpUser()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##Type## ##HomeDir##
        implement ToString Name=##Name##, Type=##Type##, HomeDir=##HomeDir##
        implement equals Name, Type, HomeDir
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Name; Nazwa użytkownika FTP
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności Type; Typ użytkownika
        /// </summary>
        public const string PROPERTYNAME_TYPE = "Type";
        /// <summary>
        /// Nazwa własności HomeDir; katalog domowy
        /// </summary>
        public const string PROPERTYNAME_HOMEDIR = "HomeDir";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0} ({1}) at {2}", name, type, homeDir);
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// Nazwa użytkownika FTP
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == name) return;
                name = value;
                NotifyPropertyChanged(PROPERTYNAME_NAME);
            }
        }
        private string name = string.Empty;
        /// <summary>
        /// Typ użytkownika
        /// </summary>
        public FtpUserType Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value == type) return;
                type = value;
                NotifyPropertyChanged(PROPERTYNAME_TYPE);
            }
        }
        private FtpUserType type;
        /// <summary>
        /// katalog domowy
        /// </summary>
        public string HomeDir
        {
            get
            {
                return homeDir;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == homeDir) return;
                homeDir = value;
                NotifyPropertyChanged(PROPERTYNAME_HOMEDIR);
            }
        }
        private string homeDir = string.Empty;
        #endregion Properties

    }
}
