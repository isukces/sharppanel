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
    implement Constructor Username
    implement INotifyPropertyChanged_Passive
    implement ToString ##Username##
    
    property Username String Nazwa użytkownika
    smartClassEnd
    */

    public partial class MysqlUser : NotifyPropertyChangedBase
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-06 10:39
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class MysqlUser
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public MysqlUser()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Username##
        implement ToString Username=##Username##
        implement equals Username
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public MysqlUser()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Username">Nazwa użytkownika</param>
        /// </summary>
        public MysqlUser(String Username)
        {
            this.Username = Username;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Username; Nazwa użytkownika
        /// </summary>
        public const string PROPERTYNAME_USERNAME = "Username";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", username);
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// Nazwa użytkownika
        /// </summary>
        public String Username
        {
            get
            {
                return username;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == username) return;
                username = value;
                NotifyPropertyChanged(PROPERTYNAME_USERNAME);
            }
        }
        private String username = string.Empty;
        #endregion Properties

    }
}
