using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace isukces.sharppanel
{
    public partial class ApiFunctions : BasicApi
    {
        #region Constructors

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="URL">URL bazowy cpanela</param>
        /// <param name="Username">Nazwa użytkownika</param>
        /// <param name="Password">hasło</param>
        /// </summary>
        public ApiFunctions(string URL, string Username, string Password)
            : base(URL, Username, Password)
        {
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        /// <summary>
        /// 	Create an addon domain. This function will also create a subdomain. 
        /// 	Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="dir">The path that will serve as the addon domain's home directory</param>
        /// <param name="newdomain">The domain name of the addon domain you wish to create. (e.g. sub.example.com).</param>
        /// <param name="subdomain">This value is the subdomain and FTP username corresponding to the new addon domain.</param>
        public CpanelApiResult Call_AddonDomain_Add(string dir, string newdomain, string subdomain)
        {
            if (string.IsNullOrEmpty(subdomain))
                throw new ArgumentNullException("subdomain");
            cache.Remove(CC_AddonDomain_List);
            cache.Remove(CC_BaseDomains_List);
            return Call2("AddonDomain::addaddondomain", "dir", dir, "newdomain", newdomain, "subdomain", subdomain);
        }

        /// <summary>
        /// 	Delete an existing addon domain. This function will also remove the addon domain's subdomain and corresponding FTP username. 
        /// 	Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="domain">The addon domain you wish to delete.</param>
        /// <param name="subdomain">This value should contain the addon domain's username followed by an underscore (_), then the addon domain's main domain (e.g. addonuser_maindomain.com).</param>
        public CpanelApiResult Call_AddonDomain_Del(string domain, string subdomain)
        {
            cache.Remove(CC_AddonDomain_List);
            cache.Remove(CC_SubDomain_List);
            return Call2("AddonDomain::deladdondomain", "domain", domain, "subdomain", subdomain);
        }

        /// <summary>
        ///     List all of the addon domains associated with your server.,
        ///     Full description: http://docs.cpanel.net 
        /// </summary>
        public AddonDomain[] Call_AddonDomain_List(bool skipCache = false)
        {
            return ReadCachedData(CC_AddonDomain_List, "AddonDomain::listaddondomains",
                AddonDomain.FromXElement,
                skipCache);
        }

        public bool Call_Email_editquota(string email, int quota)
        {
            Match m = Regex.Match(email, "^\\s*([^@]+)@([^@]+)\\s*$");
            if (!m.Success)
                throw new ArgumentException("email");
            return Call_Email_editquota(m.Groups[2].Value, m.Groups[1].Value, quota);
        }

        public bool Call_Email_editquota(string domain, string email, int quota)
        {
            cache.Remove(CC_Email_ListEx + "@" + ActionUsername);
            var a = Call2("Email::editquota", "domain", domain, "email", email, "quota", quota.ToString());
            return a.Success;
        }

        /// <summary>
        /// 	Retrieve information about a specific email account's disk usage. 
        /// 	Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        public double Call_Email_getdiskusage(string email)
        {
            Match m = Regex.Match(email, "^\\s*([^@]+)@([^@]+)\\s*$");
            if (!m.Success)
                throw new ArgumentException("email");
            return Call_Email_getdiskusage(m.Groups[2].Value, m.Groups[1].Value);
        }

        /// <summary>
        /// 	Retrieve information about a specific email account's disk usage. 
        /// 	Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="domain">The domain that corresponds to the email address whose disk usage information you wish to view. This value needs to be the section of the email address after the 'at' (@) sign (e.g. example.com).</param>
        /// <param name="login">The username section of the email address whose disk usage information you wish to view. This value needs to be the section of the email address before the 'at' (@) sign (e.g. user).</param>
        /// <returns></returns>
        public double Call_Email_getdiskusage(string domain, string login)
        {
            var r = Call2("Email::getdiskusage", "domain", domain, "login", login);
            var element = r.GetFirstDataElement("diskused");
            if (element == null)
                return 0;
            return getDouble(element);
        }

        public Email[] Call_Email_List(bool skipCache = false)
        {
            return ReadCachedData(CC_Email_List + "@" + ActionUsername, "Email::listpops",
               node => new Email(node.Element("email").Value, node.Element("login").Value),
               skipCache);
        }

        public EmailEx[] Call_Email_ListEx(bool skipCache = false)
        {
            return ReadCachedData(CC_Email_ListEx + "@" + ActionUsername, "Email::listpopswithdisk", EmailEx.FromXElement, skipCache);
        }


        /// <summary>
        /// Add a new FTP account. This function is only available in cPanel 11.27.x and later.
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="user">The name of the new FTP account. When authenticating with this name, remember to append the main domain to the end (e.g., user@example.com).</param>
        /// <param name="pass">The password for the new FTP account.</param>
        /// <param name="quota">The new FTP account's quota. 0 indicates that the account will not use a quota. This parameter defaults to 0.</param>
        /// <param name="homedir">The path to the FTP account's root directory. This value should be relative to the cPanel account's public_html directory.</param>
        /// <remarks>
        /// <para>domain.BaseDir działa jako komedir dla użytkownika ftp</para>
        /// </remarks>
        /// <returns></returns>
        public CpanelApiResult Call_Ftp_Add(string user, string pass, int quota, string homedir)
        {
            cache.Remove(CC_FTPUser_List);
            cache.Remove(CC_FTPUser_List_ex);
            return Call2("Ftp::addftp", "user", user, "pass", pass, "quota", quota.ToString(), "homedir", homedir);
        }

        /// <summary>
        /// 	Delete an FTP account. This function is only available in cPanel 11.27.x and later. 
        /// 	Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="user">The name of the FTP account to be removed.</param>
        /// <param name="destroy">A boolean value that indicates whether or not the FTP account's home directory should also be deleted. A value of 1 indicates that the directory should be removed. This value defaults to 0.</param>
        /// <returns></returns>
        public CpanelApiResult Call_Ftp_Del(string user, bool destroy = false)
        {
            cache.Remove(CC_FTPUser_List);
            cache.Remove(CC_FTPUser_List_ex);
            return Call2("Ftp::delftp", "user", user, "destroy", destroy ? "1" : "0");
        }

        public FtpUser[] Call_Ftp_List(bool skipCache = false)
        {
            Func<CpanelApiResult> f = () => Call2("Ftp::listftp",
                "include_acct_types", FtpUser.AllTypes, "skip_acct_types", "");
            return ReadCachedData(CC_FTPUser_List, f, FtpUser.FromXElement, skipCache);
        }

        /// <summary>
        /// Generate a list of FTP accounts associated with a cPanel account. 
        /// The list will contain each account's disk information. 
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <returns></returns>
        public FtpUserExtended[] Call_Ftp_ListEx(bool skipCache = false)
        {
            Func<CpanelApiResult> f = () => Call2("Ftp::listftpwithdisk",
                "dirhtml", "", "include_acct_types", FtpUser.AllTypes, "skip_acct_types", "");
            return ReadCachedData(CC_FTPUser_List_ex, f, FtpUserExtended.FromXElement, skipCache);
        }

        /// <summary>
        /// Change an FTP account's password. This function is only available in cPanel 11.27.x and later. 
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="user">The name of the FTP account whose password should be changed.</param>
        /// <param name="pass">The new password for the FTP account.</param>
        /// <returns></returns>
        public CpanelApiResult Call_Ftp_Passwd(string user, string pass)
        {
            return Call2("Ftp::passwd", "user", user, "pass", pass);
        }

        /// <summary>
        /// Change an FTP account's quota. This function is only available in cPanel 11.27.x and later.
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="user">The name of the FTP account whose quota should be changed.</param>
        /// <param name="quota">The new quota (in megabytes) for the FTP account.</param>
        /// <returns></returns>
        public CpanelApiResult Call_Ftp_Setquota(string user, int quota)
        {
            cache.Remove(CC_FTPUser_List_ex); // tu jest info o quota
            return Call2("Ftp::setquota", "user", user, "quota", quota.ToString());
        }

        /// <summary>
        /// Add a new MySQL database to a cPanel account.
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="dbname">The name of the MySQL database to add. The cPanel account's username will automatically be prepended to the name of the database (e.g., entering 'dbname' would result in 'cpuser_dbname').</param>
        public void Call_Mysql_adddb(string dbname)
        {
            /*
             <cpanelresult>
  <module>Mysql</module>
  <func>adddb</func>
  <type>event</type>
  <source>internal</source>
  <apiversion>1</apiversion>
  <data>
    <result></result>
  </data>
  <event>
    <result>1</result>
  </event>
</cpanelresult>
             * 
             * 
             * <cpanelresult>
  <module>Mysql</module>
  <func>adddb</func>
  <type>event</type>
  <source>internal</source>
  <apiversion>1</apiversion>
  <data>
    <result></result>
  </data>
  <event>
    <result>1</result>
  </event>
  <error>Ta nazwa bazy danych już istnieje.</error>
</cpanelresult>
             */
            const string FN = "Mysql::adddb";
            var x_document = Call1(FN, dbname);
            var error = x_document.OriginalResult.Descendants("error").FirstOrDefault();
            if (error != null && !string.IsNullOrEmpty(error.Value))
                throw new CPanelException(error.Value, FN);
        }

        /// <summary>
        /// Create a new MySQL user. 
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="username">The MySQL user to create. The account's username will automatically be prepended to this value (e.g., entering 'user' here would result in 'cpuser_user').</param>
        /// <param name="password">	The password for the new MySQL user.</param>
        public void Call_Mysql_adduser(string username, string password)
        {
            var x_document = Call1("Mysql::adduser", username, password);
            var error = x_document.OriginalResult.Descendants("error").FirstOrDefault();
            if (error != null && !string.IsNullOrEmpty(error.Value))
                throw new Exception(error.Value);

        }

        /// <summary>
        /// Grant a user permission to access a database within a cPanel account. 
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="dbname">The name of the database to allow the user to access.</param>
        /// <param name="dbuser">The MySQL user who should be given access to the database.</param>
        /// <param name="perm">A space-separated list of permissions to grant to the user (e.g., "all" or "alter drop create delete insert update lock" ).</param>
        public void Call_Mysql_adduserdb(string dbname, string dbuser, MysqlUserdbprivs perm)
        {
            var x = Call1("Mysql::adduserdb", dbname, dbuser, perm.GetAsString());
        }

        /// <summary>
        /// 	Force an update of MySQL privileges and passwords. 
        /// 	Full description: http://docs.cpanel.net 
        /// </summary>
        public void Call_Mysql_updateprivs()
        {
            var xDocument = Call1("Mysql::updateprivs");
        }

        public MysqlDB[] Call_MysqlFE_listdbs()
        {
            var xDocument = Call2("MysqlFE::listdbs");
            var t = xDocument.GetResult(i => MysqlDB.FromXElement(i));
            return t;
        }

        public string[] Call_MysqlFE_listusers()
        {
            var xDocument = Call2("MysqlFE::listusers");
            var t = xDocument.GetResult<string>((i) => i.Element("user").Value);
            return t;
        }

        /// <summary>
        ///  Retrieve a list of remote MySQL connection hosts. 
        ///  Full description: http://docs.cpanel.net 
        /// </summary>
        public string[] Call_MysqlFE_listhosts()
        {
            var xDocument = Call2("MysqlFE::listhosts");
            return xDocument.OriginalResult.Descendants("host").Select(q => q.Value).ToArray();
        }

        /// <summary>
        /// Retrieve a list of permissions that correspond to a specific user and database. 
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="db">The database that corresponds to the user whose permissions you wish to view.</param>
        /// <param name="user">	The user whose permissions you wish to view.</param>
        public MysqlUserdbprivs Call_MysqlFE_userdbprivs(string db, string user)
        {
            var xDocument = Call2("MysqlFE::userdbprivs", "db", db, "user", user);
            var d = xDocument.OriginalResult.Descendants("data").First();
            return MysqlUserdbprivs.FromXElement(d);

        }

        /// <summary>
        /// Park a domain on top of another domain. 
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="domain">The domain name you wish to park.</param>
        /// <param name="topdomain">The domain on top of which the parked domain will be parked. This value must point to a preexisting subdomain (e.g. topdomain.example.com). If this parameter is not specified, the primary domain is used.</param>
        public CpanelApiResult Call_ParkedDomain_Add(string domain, string topdomain = null)
        {
            cache.Remove(CC_BaseDomains_List);
            cache.Remove(CC_ParkedDomain_List);
            var xDocument = Call2("Park::park", "domain", domain, "topdomain", topdomain);
            return xDocument;
        }

        public ParkedDomain[] Call_ParkedDomain_List(bool skipCache = false)
        {
            return ReadCachedData(CC_ParkedDomain_List, "Park::listparkeddomains",
                ParkedDomain.FromXElement, skipCache);
        }
        /// <summary>
        /// Powinno dawać to samo co Call_AddonDomain_List
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <returns></returns>
        public AddonDomain[] Call_Park_listaddondomains()
        {
            CpanelApiResult x = Call2("Park::listaddondomains");
            var cc = x.GetResult(AddonDomain.FromXElement);           
            return cc;
        }

        public string[] Call_Resellers_accountlistopt()
        {
            var ac = ActionUsername;
            try
            {
                ActionUsername = Username;
                var result = Call1("Resellers::accountlistopt");
                var result2 = (result.XData.ToArray())[0].Element("result").Value;
                XElement el = XElement.Parse("<a>" + result2 + "</a>");
                var query = from element in el.Descendants("option")
                            let accountName = (string)element.Attribute("value")
                            orderby accountName
                            select accountName;
                return query.ToArray();
            }
            finally
            {
                ActionUsername = ac;
            }
        }

        public Statistics Call_StatsBar()
        {
            const string display = "ftpaccounts|perlversion|dedicatedip|hostname|operatingsystem|sendmailpath|autoresponders|perlpath|emailforwarders|bandwidthusage|emailfilters|mailinglists|diskusage|phpversion|sqldatabases|apacheversion|kernelversion|shorthostname|parkeddomains|cpanelbuild|theme|addondomains|cpanelrevision|machinetype|cpanelversion|mysqldiskusage|mysqlversion|subdomains|postgresdiskusage|sharedip|hostingpackage|emailaccounts";
            var t = Call2("StatsBar::stat", "display", display);
            return Statistics.FromResult(t);
        }

        /// <summary>
        /// Retrieve parked domains, addon domains, and main domains for a cPanel account. 
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <returns>parked domains, addon domains, and main domains</returns>
        public string[] Call_DomainLookup_GetBaseDomains(bool skipCache = false)
        {
            Func<XElement, string> func = new Func<XElement, string>(q => q.Element("domain").Value);
            return ReadCachedData(CC_BaseDomains_List, "DomainLookup::getbasedomains",
          func, skipCache);
        }

        /// <summary>
        /// Retrieve the absolute and relative paths to a specific domain's document root.
        /// Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="domain">The domain corresponding to the document root you wish to know. If you do not pass this parameter any information, your main domain is used. You must own the domain you wish to query.</param>
        /// <returns></returns>
        public string Call_DomainLookup_getdocroot(string domain)
        {
            var t = Call2("DomainLookup::getdocroot", "domain", domain);
            return t.GetFirstDataElement("docroot").Value;
        }


        public RootDomain[] Call_DomainLookup_GetDocRoots()
        {
            var t = Call2("DomainLookup::getdocroots");
            return t.GetResult(RootDomain.FromXElement);
            /*
             <cpanelresult>
  <apiversion>2</apiversion>
  <data>
    <docroot>/home/company_names/public_html</docroot>
    <domain>companyname.provider.com</domain>
  </data>
  <data>
    <docroot>/home/company_names/public_html</docroot>
    <domain>jakastestowadomena.pl</domain>
  </data>
  <event>
    <result>1</result>
  </event>
  <func>getdocroots</func>
  <module>DomainLookup</module>
</cpanelresult>             
             */


        }
        /// <summary>
        ///     Add a subdomain. 
        ///     Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="dir">The subdomain's document root within your home directory. This value defaults to a directory of the same name as the subdomain in your public_html directory. (e.g. public_html/subdomain/ if the domain's name was subdomain.example.com).</param>
        /// <param name="disallowdot">If this parameter is enabled (set to '1'), it will automatically strip dots from the 'domain' value passed to this function.</param>
        /// <param name="domain">	The local part of the subdomain you wish to add. (e.g. 'sub' if the subdomain's is sub.example.com) This value should not include the domain with which the subdomain is associated.</param>
        /// <param name="rootdomain">The domain to which you wish to add the subdomain.</param>
        public CpanelApiResult Call_SubDomain_Add(string dir, bool disallowdot, string domain, string rootdomain)
        {
            cache.Remove(CC_SubDomain_List);
            return Call2("SubDomain::addsubdomain", "dir", dir, "disallowdot", disallowdot ? "1" : "0", "domain", domain, "rootdomain", rootdomain);
        }

        /// <summary>
        /// 	Delete a subdomain. 
        /// 	Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="domain">The subdomain you wish to delete.</param>
        public CpanelApiResult Call_SubDomain_Del(string domain)
        {
            cache.Remove(CC_SubDomain_List);
            return Call2("SubDomain::delsubdomain", "domain", domain);
        }

        /// <summary>
        ///     Delete a subdomain
        ///     Full description: http://docs.cpanel.net 
        /// </summary>
        /// <param name="domain">The subdomain you wish to delete.</param>
        public void Call_SubDomain_Del(SubDomain domain)
        {
            Call_SubDomain_Del(domain.Domain);
        }

        public SubDomain[] Call_SubDomain_List(bool skipCache = false)
        {
            return ReadCachedData(CC_SubDomain_List, "SubDomain::listsubdomains",
                SubDomain.FromXElement,
                skipCache);
        }

        #endregion Methods

        #region Fields

        public const string CC_BaseDomains_List = "BaseDomains";
        public const string CC_AddonDomain_List = "AddonDomains";
        public const string CC_Email_List = "Emails";
        public const string CC_Email_ListEx = "EmailsEx";
        public const string CC_FTPUser_List = "FTPUser_List";
        public const string CC_FTPUser_List_ex = "FTPUser_List_ex";
        public const string CC_ParkedDomain_List = "ParkedDomains";
        public const string CC_SubDomain_List = "SubDomains";
        public const double ExpireSeconds = 15;
        // private CredentialCache MyCredential;

        #endregion Fields
    }
}
