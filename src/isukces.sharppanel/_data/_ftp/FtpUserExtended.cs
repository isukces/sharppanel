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
    
    property DiskquotaBytes double An integer value that contains the FTP account's disk space quota in bytes.
    
    property DiskusedBytes double An integer value that contains the amount of disk space used by the FTP account in bytes.
    
    property Deleteable bool A boolean value that indicates whether or not the account can be deleted. '1' if you can delete the account; '0' if you cannot.
    
    property Diskquota double A string value that contains the account's disk quota in megabytes.
    
    property Diskused double An integer value that contains the disk space used by the account in megabytes
    
    property Diskusedpercent double An integer value that contains the percentage of available disk space used by the FTP account.
    
    property Diskusedpercent20 double An integer value that contains the percentage of disk space the FTP account has used.
    
    property HtmlDir string HtmlDir - nie ma opisu
    
    property HumanDiskQuota string A string value that contains the FTP account's disk quota in megabytes. This value contains the megabyte (MB) descriptor. (e.g. 250 MB)
    
    property HumanDiskUsed string An integer value that contains the amount of disk space used by the FTP account in expressed in megabytes. This value contains the megabyte (MB) descriptor. (e.g. 250 MB)
    
    property Reldir string A string value that contains the relative path to the FTP account's document root. This value will contain the 'home:' prefix. (e.g. home:public_html/ftp/)
    
    property ServerLogin string A string value that contains the full FTP login name. (e.g. user@example.com)
    smartClassEnd
    */

    public partial class FtpUserExtended : FtpUser
    {
        #region Static Methods

        // Public Methods 

        public new static FtpUserExtended FromXElement(XElement x)
        {
            return new FtpUserExtended()
            {
                DiskquotaBytes = parseDouble(x.Element("_diskquota").Value),
                DiskusedBytes = parseDouble(x.Element("_diskused").Value),
                Type = (FtpUserType)Enum.Parse(typeof(FtpUserType), x.Element("accttype").Value, true),
                Deleteable = x.Element("deleteable").Value.Trim() != "0",
                HomeDir = x.Element("dir").Value,
                Diskquota = parseDouble(x.Element("diskquota").Value),
                Diskused = parseDouble(x.Element("diskused").Value),
                Diskusedpercent = parseDouble(x.Element("diskusedpercent").Value),
                Diskusedpercent20 = parseDouble(x.Element("diskusedpercent20").Value),
                HtmlDir = x.Element("htmldir").Value,
                HumanDiskQuota = x.Element("humandiskquota").Value,
                HumanDiskUsed = x.Element("humandiskused").Value,
                Name = x.Element("login").Value,
                Reldir = x.Element("reldir").Value,
                ServerLogin = x.Element("serverlogin").Value,
            };
        }
        // Private Methods 



        #endregion Static Methods

        #region Methods

        // Public Methods 

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0} at {1} ({2} / of {3})", Name, HomeDir, humanDiskUsed, diskquota);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-09 14:15
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class FtpUserExtended
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public FtpUserExtended()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##DiskquotaBytes## ##DiskusedBytes## ##Deleteable## ##Diskquota## ##Diskused## ##Diskusedpercent## ##Diskusedpercent20## ##HtmlDir## ##HumanDiskQuota## ##HumanDiskUsed## ##Reldir## ##ServerLogin##
        implement ToString DiskquotaBytes=##DiskquotaBytes##, DiskusedBytes=##DiskusedBytes##, Deleteable=##Deleteable##, Diskquota=##Diskquota##, Diskused=##Diskused##, Diskusedpercent=##Diskusedpercent##, Diskusedpercent20=##Diskusedpercent20##, HtmlDir=##HtmlDir##, HumanDiskQuota=##HumanDiskQuota##, HumanDiskUsed=##HumanDiskUsed##, Reldir=##Reldir##, ServerLogin=##ServerLogin##
        implement equals DiskquotaBytes, DiskusedBytes, Deleteable, Diskquota, Diskused, Diskusedpercent, Diskusedpercent20, HtmlDir, HumanDiskQuota, HumanDiskUsed, Reldir, ServerLogin
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności DiskquotaBytes; An integer value that contains the FTP account's disk space quota in bytes.
        /// </summary>
        public const string PROPERTYNAME_DISKQUOTABYTES = "DiskquotaBytes";
        /// <summary>
        /// Nazwa własności DiskusedBytes; An integer value that contains the amount of disk space used by the FTP account in bytes.
        /// </summary>
        public const string PROPERTYNAME_DISKUSEDBYTES = "DiskusedBytes";
        /// <summary>
        /// Nazwa własności Deleteable; A boolean value that indicates whether or not the account can be deleted. '1' if you can delete the account; '0' if you cannot.
        /// </summary>
        public const string PROPERTYNAME_DELETEABLE = "Deleteable";
        /// <summary>
        /// Nazwa własności Diskquota; A string value that contains the account's disk quota in megabytes.
        /// </summary>
        public const string PROPERTYNAME_DISKQUOTA = "Diskquota";
        /// <summary>
        /// Nazwa własności Diskused; An integer value that contains the disk space used by the account in megabytes
        /// </summary>
        public const string PROPERTYNAME_DISKUSED = "Diskused";
        /// <summary>
        /// Nazwa własności Diskusedpercent; An integer value that contains the percentage of available disk space used by the FTP account.
        /// </summary>
        public const string PROPERTYNAME_DISKUSEDPERCENT = "Diskusedpercent";
        /// <summary>
        /// Nazwa własności Diskusedpercent20; An integer value that contains the percentage of disk space the FTP account has used.
        /// </summary>
        public const string PROPERTYNAME_DISKUSEDPERCENT20 = "Diskusedpercent20";
        /// <summary>
        /// Nazwa własności HtmlDir; HtmlDir - nie ma opisu
        /// </summary>
        public const string PROPERTYNAME_HTMLDIR = "HtmlDir";
        /// <summary>
        /// Nazwa własności HumanDiskQuota; A string value that contains the FTP account's disk quota in megabytes. This value contains the megabyte (MB) descriptor. (e.g. 250 MB)
        /// </summary>
        public const string PROPERTYNAME_HUMANDISKQUOTA = "HumanDiskQuota";
        /// <summary>
        /// Nazwa własności HumanDiskUsed; An integer value that contains the amount of disk space used by the FTP account in expressed in megabytes. This value contains the megabyte (MB) descriptor. (e.g. 250 MB)
        /// </summary>
        public const string PROPERTYNAME_HUMANDISKUSED = "HumanDiskUsed";
        /// <summary>
        /// Nazwa własności Reldir; A string value that contains the relative path to the FTP account's document root. This value will contain the 'home:' prefix. (e.g. home:public_html/ftp/)
        /// </summary>
        public const string PROPERTYNAME_RELDIR = "Reldir";
        /// <summary>
        /// Nazwa własności ServerLogin; A string value that contains the full FTP login name. (e.g. user@example.com)
        /// </summary>
        public const string PROPERTYNAME_SERVERLOGIN = "ServerLogin";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// An integer value that contains the FTP account's disk space quota in bytes.
        /// </summary>
        public double DiskquotaBytes
        {
            get
            {
                return diskquotaBytes;
            }
            set
            {
                if (value == diskquotaBytes) return;
                diskquotaBytes = value;
                NotifyPropertyChanged(PROPERTYNAME_DISKQUOTABYTES);
            }
        }
        private double diskquotaBytes;
        /// <summary>
        /// An integer value that contains the amount of disk space used by the FTP account in bytes.
        /// </summary>
        public double DiskusedBytes
        {
            get
            {
                return diskusedBytes;
            }
            set
            {
                if (value == diskusedBytes) return;
                diskusedBytes = value;
                NotifyPropertyChanged(PROPERTYNAME_DISKUSEDBYTES);
            }
        }
        private double diskusedBytes;
        /// <summary>
        /// A boolean value that indicates whether or not the account can be deleted. '1' if you can delete the account; '0' if you cannot.
        /// </summary>
        public bool Deleteable
        {
            get
            {
                return deleteable;
            }
            set
            {
                if (value == deleteable) return;
                deleteable = value;
                NotifyPropertyChanged(PROPERTYNAME_DELETEABLE);
            }
        }
        private bool deleteable;
        /// <summary>
        /// A string value that contains the account's disk quota in megabytes.
        /// </summary>
        public double Diskquota
        {
            get
            {
                return diskquota;
            }
            set
            {
                if (value == diskquota) return;
                diskquota = value;
                NotifyPropertyChanged(PROPERTYNAME_DISKQUOTA);
            }
        }
        private double diskquota;
        /// <summary>
        /// An integer value that contains the disk space used by the account in megabytes
        /// </summary>
        public double Diskused
        {
            get
            {
                return diskused;
            }
            set
            {
                if (value == diskused) return;
                diskused = value;
                NotifyPropertyChanged(PROPERTYNAME_DISKUSED);
            }
        }
        private double diskused;
        /// <summary>
        /// An integer value that contains the percentage of available disk space used by the FTP account.
        /// </summary>
        public double Diskusedpercent
        {
            get
            {
                return diskusedpercent;
            }
            set
            {
                if (value == diskusedpercent) return;
                diskusedpercent = value;
                NotifyPropertyChanged(PROPERTYNAME_DISKUSEDPERCENT);
            }
        }
        private double diskusedpercent;
        /// <summary>
        /// An integer value that contains the percentage of disk space the FTP account has used.
        /// </summary>
        public double Diskusedpercent20
        {
            get
            {
                return diskusedpercent20;
            }
            set
            {
                if (value == diskusedpercent20) return;
                diskusedpercent20 = value;
                NotifyPropertyChanged(PROPERTYNAME_DISKUSEDPERCENT20);
            }
        }
        private double diskusedpercent20;
        /// <summary>
        /// HtmlDir - nie ma opisu
        /// </summary>
        public string HtmlDir
        {
            get
            {
                return htmlDir;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == htmlDir) return;
                htmlDir = value;
                NotifyPropertyChanged(PROPERTYNAME_HTMLDIR);
            }
        }
        private string htmlDir = string.Empty;
        /// <summary>
        /// A string value that contains the FTP account's disk quota in megabytes. This value contains the megabyte (MB) descriptor. (e.g. 250 MB)
        /// </summary>
        public string HumanDiskQuota
        {
            get
            {
                return humanDiskQuota;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == humanDiskQuota) return;
                humanDiskQuota = value;
                NotifyPropertyChanged(PROPERTYNAME_HUMANDISKQUOTA);
            }
        }
        private string humanDiskQuota = string.Empty;
        /// <summary>
        /// An integer value that contains the amount of disk space used by the FTP account in expressed in megabytes. This value contains the megabyte (MB) descriptor. (e.g. 250 MB)
        /// </summary>
        public string HumanDiskUsed
        {
            get
            {
                return humanDiskUsed;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == humanDiskUsed) return;
                humanDiskUsed = value;
                NotifyPropertyChanged(PROPERTYNAME_HUMANDISKUSED);
            }
        }
        private string humanDiskUsed = string.Empty;
        /// <summary>
        /// A string value that contains the relative path to the FTP account's document root. This value will contain the 'home:' prefix. (e.g. home:public_html/ftp/)
        /// </summary>
        public string Reldir
        {
            get
            {
                return reldir;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == reldir) return;
                reldir = value;
                NotifyPropertyChanged(PROPERTYNAME_RELDIR);
            }
        }
        private string reldir = string.Empty;
        /// <summary>
        /// A string value that contains the full FTP login name. (e.g. user@example.com)
        /// </summary>
        public string ServerLogin
        {
            get
            {
                return serverLogin;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == serverLogin) return;
                serverLogin = value;
                NotifyPropertyChanged(PROPERTYNAME_SERVERLOGIN);
            }
        }
        private string serverLogin = string.Empty;
        #endregion Properties

    }
}
