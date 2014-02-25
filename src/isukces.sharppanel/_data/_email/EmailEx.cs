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
    
    property CustomerOwner string nazwa konta właściciela cPanela
    
    property DiskUsage ResourceD użycie dysku
    	init #
    
    property MKTime DateTimeOffset Czas utworzenia chyba
    smartClassEnd
    */

    public partial class EmailEx : Email
    {
        public static EmailEx FromXElement(XElement x)
        {
            EmailEx e = new EmailEx();
            try
            {
                e.EmailAddress = x.Element("email").Value;
                e.Login = x.Element("user").Value;
                e.DiskUsage.Count = getDouble(x.Element("_diskused"));
                if (x.Element("diskquota").Value.Trim() == "unlimited")
                    e.DiskUsage.IsUnlimited = true;
                else
                    e.DiskUsage.Limit = getDouble(x.Element("_diskquota"));
                e.DiskUsage /= 1024 * 1024;

                var xx = x.Element("mtime");
                if (xx != null)
                {
                    int mktime = int.Parse(xx.Value);
                    e.MKTime = (new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.FromTicks(0))).AddSeconds(mktime);
                }
            }
            catch
            {
                throw;
            }
            return e;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-18 17:53
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class EmailEx
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public EmailEx()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##CustomerOwner## ##DiskUsage## ##MKTime##
        implement ToString CustomerOwner=##CustomerOwner##, DiskUsage=##DiskUsage##, MKTime=##MKTime##
        implement equals CustomerOwner, DiskUsage, MKTime
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności CustomerOwner; nazwa konta właściciela cPanela
        /// </summary>
        public const string PROPERTYNAME_CUSTOMEROWNER = "CustomerOwner";
        /// <summary>
        /// Nazwa własności DiskUsage; użycie dysku
        /// </summary>
        public const string PROPERTYNAME_DISKUSAGE = "DiskUsage";
        /// <summary>
        /// Nazwa własności MKTime; Czas utworzenia chyba
        /// </summary>
        public const string PROPERTYNAME_MKTIME = "MKTime";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// nazwa konta właściciela cPanela
        /// </summary>
        public string CustomerOwner
        {
            get
            {
                return customerOwner;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == customerOwner) return;
                customerOwner = value;
                NotifyPropertyChanged(PROPERTYNAME_CUSTOMEROWNER);
            }
        }
        private string customerOwner = string.Empty;
        /// <summary>
        /// użycie dysku
        /// </summary>
        public ResourceD DiskUsage
        {
            get
            {
                return diskUsage;
            }
            set
            {
                if (value == diskUsage) return;
                diskUsage = value;
                NotifyPropertyChanged(PROPERTYNAME_DISKUSAGE);
            }
        }
        private ResourceD diskUsage = new ResourceD();
        /// <summary>
        /// Czas utworzenia chyba
        /// </summary>
        public DateTimeOffset MKTime
        {
            get
            {
                return mKTime;
            }
            set
            {
                if (value == mKTime) return;
                mKTime = value;
                NotifyPropertyChanged(PROPERTYNAME_MKTIME);
            }
        }
        private DateTimeOffset mKTime;
        #endregion Properties

    }
}
