using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    
    property Status WellKownCpanelProblems Status Błędu
    	read only
    
    property CPanelFunction string Funkcja Cpanel wywoływana w trakcie błędu
    smartClassEnd
    */

    public partial class CPanelException : Exception
    {
        #region Constructors

        public CPanelException(WellKownCpanelProblems aStatus, string f)
            : base(GetStatusDescription(aStatus))
        {
            this.status = aStatus;
            cPanelFunction = f;
        }

        public CPanelException(string message, string f)
            : base(message)
        {
            CPanelFunction = f;
        }

        public CPanelException(string message, string f, Exception inner)
            : base(message, inner)
        {
            CPanelFunction = f;
        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 

        public static string GetStatusDescription(WellKownCpanelProblems status, string df = null)
        {
            // "Brak dostępu do CPanela, prawdopodobnie zła nazwa użytkownika lub hasło"
            switch (status)
            {
                case WellKownCpanelProblems.LoginError:
                    return Polish.strBladLogowaniaDoCPanel + " " + Polish.strSprawdzNazweUzytkownikaIHaslo;
                case WellKownCpanelProblems.NameResolutionFailure:
                    return Polish.strNieZnalezionoSerweraCPanel;
                case WellKownCpanelProblems.EmptyOrInvalidCPanelURL:
                    return Polish.strAdresCPanelaJestPustyLubNieZawieraPrawidlowegoAdresuURLDlaProtokoluHttpLubHttps;
                case WellKownCpanelProblems.EmptyUsernameOrPassword:
                    return Polish.strBrakNazwyUzytkownikaLubHaslaDoCPanela;
                default:
                    return string.IsNullOrEmpty(df) ? Polish.strInnyBlad : df;
            }
        }

        #endregion Static Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-21 11:18
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class CPanelException
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public CPanelException()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Status## ##CPanelFunction##
        implement ToString Status=##Status##, CPanelFunction=##CPanelFunction##
        implement equals Status, CPanelFunction
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności Status; Status Błędu
        /// </summary>
        public const string PROPERTYNAME_STATUS = "Status";
        /// <summary>
        /// Nazwa własności CPanelFunction; Funkcja Cpanel wywoływana w trakcie błędu
        /// </summary>
        public const string PROPERTYNAME_CPANELFUNCTION = "CPanelFunction";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// Status Błędu; własność jest tylko do odczytu.
        /// </summary>
        public WellKownCpanelProblems Status
        {
            get
            {
                return status;
            }
        }
        private WellKownCpanelProblems status;
        /// <summary>
        /// Funkcja Cpanel wywoływana w trakcie błędu
        /// </summary>
        public string CPanelFunction
        {
            get
            {
                return cPanelFunction;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                cPanelFunction = value;
            }
        }
        private string cPanelFunction = string.Empty;
        #endregion Properties
    }
}
