using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    
    property CallFunction string fukcja, która jest wywoływana
    
    property Progress TransmissionMessageType etap komunikacji
    smartClassEnd
    */

    public partial class TransmissionMessage
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-10 09:57
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class TransmissionMessage
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public TransmissionMessage()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##CallFunction## ##Progress##
        implement ToString CallFunction=##CallFunction##, Progress=##Progress##
        implement equals CallFunction, Progress
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności CallFunction; fukcja, która jest wywoływana
        /// </summary>
        public const string PROPERTYNAME_CALLFUNCTION = "CallFunction";
        /// <summary>
        /// Nazwa własności Progress; etap komunikacji
        /// </summary>
        public const string PROPERTYNAME_PROGRESS = "Progress";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// fukcja, która jest wywoływana
        /// </summary>
        public string CallFunction
        {
            get
            {
                return callFunction;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                callFunction = value;
            }
        }
        private string callFunction = string.Empty;
        /// <summary>
        /// etap komunikacji
        /// </summary>
        public TransmissionMessageType Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
            }
        }
        private TransmissionMessageType progress;
        #endregion Properties

    }
}
