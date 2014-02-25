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
    
    property FtpAccounts Resource konta FTP
    
    property PerlVersion string wersja PERL
    
    property PerlPath string ścieżka Perl
    
    property Hostname string nazwa hosta
    
    property OperatingSystem string System operacyjny
    
    property SendmailPath string sendmailpath
    
    property Autoresponders Resource autoresponders
    
    property EmailForwarders Resource emailforwarders
    
    property BandwidthUsage ResourceD użycie transferu
    
    property EmailFilters Resource ilość filtrów
    
    property DiskUsage ResourceD użycie dysku
    
    property PhpVersion string wersja PHP
    
    property SqlDatabases Resource ilość baz
    
    property ApacheVersion string wersja Apache
    
    property KernelVersion string wersja jądra
    
    property ParkedDomains Resource ilość domen parkowanych
    
    property SubDomains Resource ilość poddomen
    
    property AddonDomains Resource ilość domen dodatkowych
    
    property MachineType string Typ maszyny
    
    property Theme string Skórka
    
    property CPanelVersion string cPanel Version
    
    property MysqlDiskUsage ResourceD Rozmiar baz MySQL
    
    property MysqlVersion string wersja MySQL
    
    property SharedIP string Wspólny adres IP
    
    property HostingPackage string Pakiet hostingowy
    
    property EmailAccounts Resource Konta e-mail
    
    property CustomerOwner string nazwa konta właściciela cPanela
    smartClassEnd
    */

    public partial class Statistics : NotifyPropertyChangedBase
    {
        #region Static Methods

        // Public Methods 

        public static Statistics FromResult(CpanelApiResult x)
        {
            Statistics s = new Statistics();
            s.FtpAccounts = getResource(x, "ftpaccounts");
            s.PerlVersion = getString(x, "perlversion");
            s.Hostname = getString(x, "hostname");
            s.OperatingSystem = getString(x, "operatingsystem");
            s.SendmailPath = getString(x, "sendmailpath");
            s.Autoresponders = getResource(x, "autoresponders");
            s.PerlPath = getString(x, "perlpath");
            s.EmailForwarders = getResource(x, "emailforwarders");
            s.BandwidthUsage = getResourceD(x, "bandwidthusage");
            s.EmailFilters = getResource(x, "emailfilters");

            s.DiskUsage = getResourceD(x, "diskusage");
            s.PhpVersion = getString(x, "phpversion");
            s.SqlDatabases = getResource(x, "sqldatabases");

            s.ApacheVersion = getString(x, "apacheversion");
            s.KernelVersion = getString(x, "kernelversion");

            s.ParkedDomains = getResource(x, "parkeddomains");
            s.AddonDomains = getResource(x, "addondomains");
            s.SubDomains = getResource(x, "subdomains");
            s.MachineType = getString(x, "machinetype");
            s.Theme = getString(x, "theme");
            s.CPanelVersion = getString(x, "cpanelversion");
            s.MysqlDiskUsage = getResourceD(x, "mysqldiskusage") / (1024 * 1024);
            s.MysqlVersion = getString(x, "mysqlversion");
            s.SharedIP = getString(x, "sharedip");
            s.HostingPackage = getString(x, "hostingpackage");
            s.EmailAccounts = getResource(x, "emailaccounts");

            return s;
        }
        // Private Methods 

        private static Resource getResource(CpanelApiResult x, string name)
        {
            XElement d = x.XData.Where(q => q.Element("name").Value == name).FirstOrDefault();
            if (d == null)
                return new Resource();

            var maxElement = d.Element("_max");
            var countElement = d.Element("_count");
            if (maxElement != null && maxElement.Value.Trim().ToLower() == "unlimited")
                return Resource.GetUnlimited(getInt(countElement));
            return new Resource(getInt(countElement), getInt(maxElement));
        }

        private static ResourceD getResourceD(CpanelApiResult x, string name)
        {
            XElement d = x.XData.Where(q => q.Element("name").Value == name).FirstOrDefault();
            if (d == null)
                return null;
            var maxElement = d.Element("_max");
            var countElement = d.Element("_count");
            if (maxElement != null && maxElement.Value.Trim().ToLower() == "unlimited")
                return ResourceD.GetUnlimited(getDouble(countElement));
            return new ResourceD(getDouble(countElement), getDouble(maxElement));
        }



        #endregion Static Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-18 17:53
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class Statistics
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public Statistics()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##FtpAccounts## ##PerlVersion## ##PerlPath## ##Hostname## ##OperatingSystem## ##SendmailPath## ##Autoresponders## ##EmailForwarders## ##BandwidthUsage## ##EmailFilters## ##DiskUsage## ##PhpVersion## ##SqlDatabases## ##ApacheVersion## ##KernelVersion## ##ParkedDomains## ##SubDomains## ##AddonDomains## ##MachineType## ##Theme## ##CPanelVersion## ##MysqlDiskUsage## ##MysqlVersion## ##SharedIP## ##HostingPackage## ##EmailAccounts## ##CustomerOwner##
        implement ToString FtpAccounts=##FtpAccounts##, PerlVersion=##PerlVersion##, PerlPath=##PerlPath##, Hostname=##Hostname##, OperatingSystem=##OperatingSystem##, SendmailPath=##SendmailPath##, Autoresponders=##Autoresponders##, EmailForwarders=##EmailForwarders##, BandwidthUsage=##BandwidthUsage##, EmailFilters=##EmailFilters##, DiskUsage=##DiskUsage##, PhpVersion=##PhpVersion##, SqlDatabases=##SqlDatabases##, ApacheVersion=##ApacheVersion##, KernelVersion=##KernelVersion##, ParkedDomains=##ParkedDomains##, SubDomains=##SubDomains##, AddonDomains=##AddonDomains##, MachineType=##MachineType##, Theme=##Theme##, CPanelVersion=##CPanelVersion##, MysqlDiskUsage=##MysqlDiskUsage##, MysqlVersion=##MysqlVersion##, SharedIP=##SharedIP##, HostingPackage=##HostingPackage##, EmailAccounts=##EmailAccounts##, CustomerOwner=##CustomerOwner##
        implement equals FtpAccounts, PerlVersion, PerlPath, Hostname, OperatingSystem, SendmailPath, Autoresponders, EmailForwarders, BandwidthUsage, EmailFilters, DiskUsage, PhpVersion, SqlDatabases, ApacheVersion, KernelVersion, ParkedDomains, SubDomains, AddonDomains, MachineType, Theme, CPanelVersion, MysqlDiskUsage, MysqlVersion, SharedIP, HostingPackage, EmailAccounts, CustomerOwner
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności FtpAccounts; konta FTP
        /// </summary>
        public const string PROPERTYNAME_FTPACCOUNTS = "FtpAccounts";
        /// <summary>
        /// Nazwa własności PerlVersion; wersja PERL
        /// </summary>
        public const string PROPERTYNAME_PERLVERSION = "PerlVersion";
        /// <summary>
        /// Nazwa własności PerlPath; ścieżka Perl
        /// </summary>
        public const string PROPERTYNAME_PERLPATH = "PerlPath";
        /// <summary>
        /// Nazwa własności Hostname; nazwa hosta
        /// </summary>
        public const string PROPERTYNAME_HOSTNAME = "Hostname";
        /// <summary>
        /// Nazwa własności OperatingSystem; System operacyjny
        /// </summary>
        public const string PROPERTYNAME_OPERATINGSYSTEM = "OperatingSystem";
        /// <summary>
        /// Nazwa własności SendmailPath; sendmailpath
        /// </summary>
        public const string PROPERTYNAME_SENDMAILPATH = "SendmailPath";
        /// <summary>
        /// Nazwa własności Autoresponders; autoresponders
        /// </summary>
        public const string PROPERTYNAME_AUTORESPONDERS = "Autoresponders";
        /// <summary>
        /// Nazwa własności EmailForwarders; emailforwarders
        /// </summary>
        public const string PROPERTYNAME_EMAILFORWARDERS = "EmailForwarders";
        /// <summary>
        /// Nazwa własności BandwidthUsage; użycie transferu
        /// </summary>
        public const string PROPERTYNAME_BANDWIDTHUSAGE = "BandwidthUsage";
        /// <summary>
        /// Nazwa własności EmailFilters; ilość filtrów
        /// </summary>
        public const string PROPERTYNAME_EMAILFILTERS = "EmailFilters";
        /// <summary>
        /// Nazwa własności DiskUsage; użycie dysku
        /// </summary>
        public const string PROPERTYNAME_DISKUSAGE = "DiskUsage";
        /// <summary>
        /// Nazwa własności PhpVersion; wersja PHP
        /// </summary>
        public const string PROPERTYNAME_PHPVERSION = "PhpVersion";
        /// <summary>
        /// Nazwa własności SqlDatabases; ilość baz
        /// </summary>
        public const string PROPERTYNAME_SQLDATABASES = "SqlDatabases";
        /// <summary>
        /// Nazwa własności ApacheVersion; wersja Apache
        /// </summary>
        public const string PROPERTYNAME_APACHEVERSION = "ApacheVersion";
        /// <summary>
        /// Nazwa własności KernelVersion; wersja jądra
        /// </summary>
        public const string PROPERTYNAME_KERNELVERSION = "KernelVersion";
        /// <summary>
        /// Nazwa własności ParkedDomains; ilość domen parkowanych
        /// </summary>
        public const string PROPERTYNAME_PARKEDDOMAINS = "ParkedDomains";
        /// <summary>
        /// Nazwa własności SubDomains; ilość poddomen
        /// </summary>
        public const string PROPERTYNAME_SUBDOMAINS = "SubDomains";
        /// <summary>
        /// Nazwa własności AddonDomains; ilość domen dodatkowych
        /// </summary>
        public const string PROPERTYNAME_ADDONDOMAINS = "AddonDomains";
        /// <summary>
        /// Nazwa własności MachineType; Typ maszyny
        /// </summary>
        public const string PROPERTYNAME_MACHINETYPE = "MachineType";
        /// <summary>
        /// Nazwa własności Theme; Skórka
        /// </summary>
        public const string PROPERTYNAME_THEME = "Theme";
        /// <summary>
        /// Nazwa własności CPanelVersion; cPanel Version
        /// </summary>
        public const string PROPERTYNAME_CPANELVERSION = "CPanelVersion";
        /// <summary>
        /// Nazwa własności MysqlDiskUsage; Rozmiar baz MySQL
        /// </summary>
        public const string PROPERTYNAME_MYSQLDISKUSAGE = "MysqlDiskUsage";
        /// <summary>
        /// Nazwa własności MysqlVersion; wersja MySQL
        /// </summary>
        public const string PROPERTYNAME_MYSQLVERSION = "MysqlVersion";
        /// <summary>
        /// Nazwa własności SharedIP; Wspólny adres IP
        /// </summary>
        public const string PROPERTYNAME_SHAREDIP = "SharedIP";
        /// <summary>
        /// Nazwa własności HostingPackage; Pakiet hostingowy
        /// </summary>
        public const string PROPERTYNAME_HOSTINGPACKAGE = "HostingPackage";
        /// <summary>
        /// Nazwa własności EmailAccounts; Konta e-mail
        /// </summary>
        public const string PROPERTYNAME_EMAILACCOUNTS = "EmailAccounts";
        /// <summary>
        /// Nazwa własności CustomerOwner; nazwa konta właściciela cPanela
        /// </summary>
        public const string PROPERTYNAME_CUSTOMEROWNER = "CustomerOwner";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// konta FTP
        /// </summary>
        public Resource FtpAccounts
        {
            get
            {
                return ftpAccounts;
            }
            set
            {
                if (value == ftpAccounts) return;
                ftpAccounts = value;
                NotifyPropertyChanged(PROPERTYNAME_FTPACCOUNTS);
            }
        }
        private Resource ftpAccounts;
        /// <summary>
        /// wersja PERL
        /// </summary>
        public string PerlVersion
        {
            get
            {
                return perlVersion;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == perlVersion) return;
                perlVersion = value;
                NotifyPropertyChanged(PROPERTYNAME_PERLVERSION);
            }
        }
        private string perlVersion = string.Empty;
        /// <summary>
        /// ścieżka Perl
        /// </summary>
        public string PerlPath
        {
            get
            {
                return perlPath;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == perlPath) return;
                perlPath = value;
                NotifyPropertyChanged(PROPERTYNAME_PERLPATH);
            }
        }
        private string perlPath = string.Empty;
        /// <summary>
        /// nazwa hosta
        /// </summary>
        public string Hostname
        {
            get
            {
                return hostname;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == hostname) return;
                hostname = value;
                NotifyPropertyChanged(PROPERTYNAME_HOSTNAME);
            }
        }
        private string hostname = string.Empty;
        /// <summary>
        /// System operacyjny
        /// </summary>
        public string OperatingSystem
        {
            get
            {
                return operatingSystem;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == operatingSystem) return;
                operatingSystem = value;
                NotifyPropertyChanged(PROPERTYNAME_OPERATINGSYSTEM);
            }
        }
        private string operatingSystem = string.Empty;
        /// <summary>
        /// sendmailpath
        /// </summary>
        public string SendmailPath
        {
            get
            {
                return sendmailPath;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == sendmailPath) return;
                sendmailPath = value;
                NotifyPropertyChanged(PROPERTYNAME_SENDMAILPATH);
            }
        }
        private string sendmailPath = string.Empty;
        /// <summary>
        /// autoresponders
        /// </summary>
        public Resource Autoresponders
        {
            get
            {
                return autoresponders;
            }
            set
            {
                if (value == autoresponders) return;
                autoresponders = value;
                NotifyPropertyChanged(PROPERTYNAME_AUTORESPONDERS);
            }
        }
        private Resource autoresponders;
        /// <summary>
        /// emailforwarders
        /// </summary>
        public Resource EmailForwarders
        {
            get
            {
                return emailForwarders;
            }
            set
            {
                if (value == emailForwarders) return;
                emailForwarders = value;
                NotifyPropertyChanged(PROPERTYNAME_EMAILFORWARDERS);
            }
        }
        private Resource emailForwarders;
        /// <summary>
        /// użycie transferu
        /// </summary>
        public ResourceD BandwidthUsage
        {
            get
            {
                return bandwidthUsage;
            }
            set
            {
                if (value == bandwidthUsage) return;
                bandwidthUsage = value;
                NotifyPropertyChanged(PROPERTYNAME_BANDWIDTHUSAGE);
            }
        }
        private ResourceD bandwidthUsage;
        /// <summary>
        /// ilość filtrów
        /// </summary>
        public Resource EmailFilters
        {
            get
            {
                return emailFilters;
            }
            set
            {
                if (value == emailFilters) return;
                emailFilters = value;
                NotifyPropertyChanged(PROPERTYNAME_EMAILFILTERS);
            }
        }
        private Resource emailFilters;
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
        private ResourceD diskUsage;
        /// <summary>
        /// wersja PHP
        /// </summary>
        public string PhpVersion
        {
            get
            {
                return phpVersion;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == phpVersion) return;
                phpVersion = value;
                NotifyPropertyChanged(PROPERTYNAME_PHPVERSION);
            }
        }
        private string phpVersion = string.Empty;
        /// <summary>
        /// ilość baz
        /// </summary>
        public Resource SqlDatabases
        {
            get
            {
                return sqlDatabases;
            }
            set
            {
                if (value == sqlDatabases) return;
                sqlDatabases = value;
                NotifyPropertyChanged(PROPERTYNAME_SQLDATABASES);
            }
        }
        private Resource sqlDatabases;
        /// <summary>
        /// wersja Apache
        /// </summary>
        public string ApacheVersion
        {
            get
            {
                return apacheVersion;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == apacheVersion) return;
                apacheVersion = value;
                NotifyPropertyChanged(PROPERTYNAME_APACHEVERSION);
            }
        }
        private string apacheVersion = string.Empty;
        /// <summary>
        /// wersja jądra
        /// </summary>
        public string KernelVersion
        {
            get
            {
                return kernelVersion;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == kernelVersion) return;
                kernelVersion = value;
                NotifyPropertyChanged(PROPERTYNAME_KERNELVERSION);
            }
        }
        private string kernelVersion = string.Empty;
        /// <summary>
        /// ilość domen parkowanych
        /// </summary>
        public Resource ParkedDomains
        {
            get
            {
                return parkedDomains;
            }
            set
            {
                if (value == parkedDomains) return;
                parkedDomains = value;
                NotifyPropertyChanged(PROPERTYNAME_PARKEDDOMAINS);
            }
        }
        private Resource parkedDomains;
        /// <summary>
        /// ilość poddomen
        /// </summary>
        public Resource SubDomains
        {
            get
            {
                return subDomains;
            }
            set
            {
                if (value == subDomains) return;
                subDomains = value;
                NotifyPropertyChanged(PROPERTYNAME_SUBDOMAINS);
            }
        }
        private Resource subDomains;
        /// <summary>
        /// ilość domen dodatkowych
        /// </summary>
        public Resource AddonDomains
        {
            get
            {
                return addonDomains;
            }
            set
            {
                if (value == addonDomains) return;
                addonDomains = value;
                NotifyPropertyChanged(PROPERTYNAME_ADDONDOMAINS);
            }
        }
        private Resource addonDomains;
        /// <summary>
        /// Typ maszyny
        /// </summary>
        public string MachineType
        {
            get
            {
                return machineType;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == machineType) return;
                machineType = value;
                NotifyPropertyChanged(PROPERTYNAME_MACHINETYPE);
            }
        }
        private string machineType = string.Empty;
        /// <summary>
        /// Skórka
        /// </summary>
        public string Theme
        {
            get
            {
                return theme;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == theme) return;
                theme = value;
                NotifyPropertyChanged(PROPERTYNAME_THEME);
            }
        }
        private string theme = string.Empty;
        /// <summary>
        /// cPanel Version
        /// </summary>
        public string CPanelVersion
        {
            get
            {
                return cPanelVersion;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == cPanelVersion) return;
                cPanelVersion = value;
                NotifyPropertyChanged(PROPERTYNAME_CPANELVERSION);
            }
        }
        private string cPanelVersion = string.Empty;
        /// <summary>
        /// Rozmiar baz MySQL
        /// </summary>
        public ResourceD MysqlDiskUsage
        {
            get
            {
                return mysqlDiskUsage;
            }
            set
            {
                if (value == mysqlDiskUsage) return;
                mysqlDiskUsage = value;
                NotifyPropertyChanged(PROPERTYNAME_MYSQLDISKUSAGE);
            }
        }
        private ResourceD mysqlDiskUsage;
        /// <summary>
        /// wersja MySQL
        /// </summary>
        public string MysqlVersion
        {
            get
            {
                return mysqlVersion;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == mysqlVersion) return;
                mysqlVersion = value;
                NotifyPropertyChanged(PROPERTYNAME_MYSQLVERSION);
            }
        }
        private string mysqlVersion = string.Empty;
        /// <summary>
        /// Wspólny adres IP
        /// </summary>
        public string SharedIP
        {
            get
            {
                return sharedIP;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == sharedIP) return;
                sharedIP = value;
                NotifyPropertyChanged(PROPERTYNAME_SHAREDIP);
            }
        }
        private string sharedIP = string.Empty;
        /// <summary>
        /// Pakiet hostingowy
        /// </summary>
        public string HostingPackage
        {
            get
            {
                return hostingPackage;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == hostingPackage) return;
                hostingPackage = value;
                NotifyPropertyChanged(PROPERTYNAME_HOSTINGPACKAGE);
            }
        }
        private string hostingPackage = string.Empty;
        /// <summary>
        /// Konta e-mail
        /// </summary>
        public Resource EmailAccounts
        {
            get
            {
                return emailAccounts;
            }
            set
            {
                if (value == emailAccounts) return;
                emailAccounts = value;
                NotifyPropertyChanged(PROPERTYNAME_EMAILACCOUNTS);
            }
        }
        private Resource emailAccounts;
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
        #endregion Properties

    }
}
