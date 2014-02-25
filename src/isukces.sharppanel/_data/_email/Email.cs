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
    implement Constructor EmailAddress, login
    implement INotifyPropertyChanged_Passive
    implement ToString ##EmailAddress##
    
    property EmailAddress string email
    
    property Login string login
    smartClassEnd
    */
    
    public partial class Email : NotifyPropertyChangedBase
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-08 16:21
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class Email 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public Email()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##EmailAddress## ##Login##
        implement ToString EmailAddress=##EmailAddress##, Login=##Login##
        implement equals EmailAddress, Login
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public Email()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="EmailAddress">email</param>
        /// <param name="Login">login</param>
        /// </summary>
        public Email(string EmailAddress, string Login)
        {
            this.EmailAddress = EmailAddress;
            this.Login = Login;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności EmailAddress; email
        /// </summary>
        public const string PROPERTYNAME_EMAILADDRESS = "EmailAddress";
        /// <summary>
        /// Nazwa własności Login; login
        /// </summary>
        public const string PROPERTYNAME_LOGIN = "Login";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", emailAddress);
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// email
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == emailAddress) return;
                emailAddress = value;
                NotifyPropertyChanged(PROPERTYNAME_EMAILADDRESS);
            }
        }
        private string emailAddress = string.Empty;
        /// <summary>
        /// login
        /// </summary>
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == login) return;
                login = value;
                NotifyPropertyChanged(PROPERTYNAME_LOGIN);
            }
        }
        private string login = string.Empty;
        #endregion Properties

    }
}
