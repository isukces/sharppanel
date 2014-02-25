using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    implement INotifyPropertyChanged_Passive
    
    property Name string nazwa dostawcy usług
    
    property HostingUpgradeUrl string URL, gdzie klient może powiększyć pakiet usług
    smartClassEnd
    */

    public partial class HostingProvider : NotifyPropertyChangedBase
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-12 08:55
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class HostingProvider
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public HostingProviderInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##HostingUpgradeUrl##
        implement ToString Name=##Name##, HostingUpgradeUrl=##HostingUpgradeUrl##
        implement equals Name, HostingUpgradeUrl
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Name; nazwa dostawcy usług
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności HostingUpgradeUrl; URL, gdzie klient może powiększyć pakiet usług
        /// </summary>
        public const string PROPERTYNAME_HOSTINGUPGRADEURL = "HostingUpgradeUrl";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// nazwa dostawcy usług
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
        /// URL, gdzie klient może powiększyć pakiet usług
        /// </summary>
        public string HostingUpgradeUrl
        {
            get
            {
                return hostingUpgradeUrl;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == hostingUpgradeUrl) return;
                hostingUpgradeUrl = value;
                NotifyPropertyChanged(PROPERTYNAME_HOSTINGUPGRADEURL);
            }
        }
        private string hostingUpgradeUrl = string.Empty;
        #endregion Properties

    }
}
