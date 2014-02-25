using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;


namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor URL, Username, Password
    implement INotifyPropertyChanged_Passive
    
    property URL string URL bazowy cpanela
    
    property Username string Nazwa użytkownika logującego się
    
    property ActionUsername string Nazwa użytkownika, w konteście którego wykonywania jest akcja
    
    property Password string hasło
    
    property Provider HostingProvider opcjonalna informacja o dostawcy usług hostingowych
    smartClassEnd
    */

    public partial class BasicApi : NotifyPropertyChangedBase, IObservable<TransmissionMessage>
    {
        #region Static Methods

        // Private Methods 

        private static string EncodeHttpParameters(string[] parameters, Dictionary<string, string> dict)
        {
            for (int i = 1; i < parameters.Length; i += 2)
                dict[parameters[i - 1]] = parameters[i];
            var pm = string.Join("&", dict.Select(q => string.Format("{0}={1}", HttpUtility.UrlEncode(q.Key), HttpUtility.UrlEncode(q.Value))));
            return pm;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public string InterpreteException(Exception e)
        {
            WellKownCpanelProblems problem;
            return InterpreteException(e, out problem);
        }

        public string InterpreteException(Exception e, out WellKownCpanelProblems problem)
        {
            problem = WellKownCpanelProblems.Unknown;
            if (e is CPanelException)
            {
                problem = (e as CPanelException).Status;
                return e.Message;
            }
            else if (e is WebException)
            {
                WebException we = e as WebException;
                switch (we.Status)
                {
                    case WebExceptionStatus.ProtocolError:
                        HttpWebResponse wr = we.Response as HttpWebResponse;
                        if (wr != null)
                        {
                            if (wr.StatusCode == HttpStatusCode.Forbidden)
                            {
                                problem = WellKownCpanelProblems.LoginError;
                                return CPanelException.GetStatusDescription(problem);
                            }
                            return string.Format(Polish.strBladProtokoluHTTP, wr.StatusCode);
                        }
                        return Polish.strInnyBladProtokoluHTTP;
                    case WebExceptionStatus.NameResolutionFailure:
                        string hostName = "";
                        try
                        {
                            Uri u = new Uri(this.uRL);
                            hostName = u.Host;
                        }
                        catch { }
                        return string.Format(Polish.strNieMoznaZnalezcSerwera0 + " " + Polish.strSprawdzNazweSerweraOrazPolaczenieZInternetem, hostName);
                    case WebExceptionStatus.ConnectFailure:
                        return string.Format(Polish.strNieMoznaPodlaczycSieDoCPanela0 + " " + Polish.strSprawdzPoprawnoscAdresuOrazPolaczenieZInternetem, uRL);
                    case WebExceptionStatus.Timeout:
                        return Polish.strSerwerNieOdpowiedzialWOczekiwanymCzasie + " " + Polish.strMozeToBycWinaSerweraLubProblemZPolaczeniemDoInternetu;
                    default:
                        return string.Format(Polish.strBladSieciowy0, we.Status);
                }
            }
            if (e is System.Net.Sockets.SocketException)
            {
                System.Net.Sockets.SocketException se = e as System.Net.Sockets.SocketException;
                switch (se.SocketErrorCode)
                {
                    case System.Net.Sockets.SocketError.HostNotFound:
                        return Polish.strNieMoznaOdnalezcSerwera;
                    case System.Net.Sockets.SocketError.ConnectionReset:
                        return Polish.strPolaczenieZostaloUtracone;
                    default:
                        return string.Format(Polish.strInnyBladPolaczenia0, se.SocketErrorCode);
                };

            }
            return string.Format(Polish.strInnyBlad0, e.Message);
        }

        public IDisposable Subscribe(IObserver<TransmissionMessage> observer)
        {
            return oh_CpanelMessage.Add(observer);
        }
        // Protected Methods 

        protected CpanelApiResult Call(string module, string function, int apiVersion, params string[] parameters)
        {
            if (string.IsNullOrEmpty(URL))
                throw new CPanelException(WellKownCpanelProblems.EmptyOrInvalidCPanelURL, "");
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                throw new CPanelException(WellKownCpanelProblems.EmptyUsernameOrPassword, "");

            Uri u;
            try
            {
                u = new Uri(URL);
            }
            catch (UriFormatException)
            {
                throw new CPanelException(WellKownCpanelProblems.EmptyOrInvalidCPanelURL, "");
            }
            if (u.Scheme.ToLower() != "http" && u.Scheme.ToLower() != "https")
                throw new CPanelException(WellKownCpanelProblems.EmptyOrInvalidCPanelURL, "");
            URL = u.AbsoluteUri;


            TransmissionMessage msg = new TransmissionMessage();
            msg.CallFunction = module + "::" + function;

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["user"] = string.IsNullOrEmpty(ActionUsername) ? Username : ActionUsername;
            dict["cpanel_xmlapi_module"] = module;
            dict["cpanel_xmlapi_func"] = function;
            dict["cpanel_xmlapi_apiversion"] = apiVersion.ToString();
            string encodedParams = EncodeHttpParameters(parameters, dict);


            string cPanelRequestURL = string.Format("{0}xml-api/cpanel?{1}", URL, encodedParams);
            var aaa = new NetworkCredential(Username, Password);
            if (MyCredential == null)
            {
                MyCredential = new CredentialCache();
                MyCredential.Add(new Uri(URL), "Basic", aaa);
            }


            WebRequest wRequest = WebRequest.Create(cPanelRequestURL);
            wRequest.PreAuthenticate = true;
            wRequest.Credentials = aaa;

            wRequest.Timeout = 1000 * 30;
            wRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(aaa.UserName + ":" + aaa.Password)));

            msg.Progress = TransmissionMessageType.RequestSending;
            oh_CpanelMessage.PushOnNext(msg);
            CpanelApiResult result = null;
            try
            {
                using (HttpWebResponse httpResponse = (HttpWebResponse)wRequest.GetResponse())
                {
                    msg.Progress = TransmissionMessageType.ResponseReading;
                    oh_CpanelMessage.PushOnNext(msg);
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        MemoryStream tmpMemoreStream = new MemoryStream(Math.Max(1024, (int)httpResponse.ContentLength));
                        byte[] copyBuffer = new byte[1024 * 8];
                        using (var stream = httpResponse.GetResponseStream())
                        {
                            while (true)
                            {
                                int bytesReaded = stream.Read(copyBuffer, 0, copyBuffer.Length);
                                if (bytesReaded == 0) break;
                                tmpMemoreStream.Write(copyBuffer, 0, bytesReaded);
                            }
                        }
                        copyBuffer = null;
                        string response = Encoding.UTF8.GetString(tmpMemoreStream.ToArray());
                        result = new CpanelApiResult(XDocument.Parse(response));
                    }
                }
            }
            catch (WebException e)
            {
                switch (e.Status)
                {
                    case WebExceptionStatus.ProtocolError:
                        var r = e.Response as HttpWebResponse;
                        if (r != null)
                        {
                            if (r.StatusCode == HttpStatusCode.Forbidden)
                                throw new CPanelException(WellKownCpanelProblems.LoginError, "");
                        }
                        break;
                    case WebExceptionStatus.NameResolutionFailure:
                        throw new CPanelException(WellKownCpanelProblems.NameResolutionFailure, "");
                }
                throw;
            }
            msg.Progress = TransmissionMessageType.Finished;
            oh_CpanelMessage.PushOnNext(msg);
            return result;
        }

        protected CpanelApiResult Call1(string moduleFuction, params string[] p)
        {
            var m = Regex.Match(moduleFuction, "^(.*)::(.*)$");
            if (!m.Success) throw new ArgumentException("mf");
            {
                List<string> x = new List<string>();
                for (int i = 0; i < p.Length; i++)
                {
                    x.Add("arg-" + i.ToString());
                    x.Add(p[i]);
                }
                p = x.ToArray();
            }

            return Call(m.Groups[1].Value, m.Groups[2].Value, 1, p);
        }

        protected CpanelApiResult Call2(string moduleFuction, params string[] p)
        {
            var m = Regex.Match(moduleFuction, "^(.*)::(.*)$");
            if (!m.Success) throw new ArgumentException("mf");
            return Call(m.Groups[1].Value, m.Groups[2].Value, 2, p);
        }

        protected T[] ReadCachedData<T>(string cn, string apiFunction, Func<XElement, T> selector, bool skipCache = false)
        {
            return ReadCachedData<T>(cn, () => Call2(apiFunction), selector, skipCache);
        }

        protected T[] ReadCachedData<T>(string cn, Func<CpanelApiResult> getF, Func<XElement, T> selector, bool skipCache = false)
        {
            CacheItem<T[]> fromCache = cache.GetFromCache<T[]>(cn);
            if (skipCache || fromCache == null || fromCache.IsExpired(10))
            {
                var result = getF();
                var t = result.GetResult(selector);
                cache.Update(cn, t);
                return t;
            }
            return fromCache.Item;
        }

        #endregion Methods

        #region Fields

        protected Cache cache = new Cache();
        private CredentialCache MyCredential;
        ObservableHelper<TransmissionMessage> oh_CpanelMessage = new ObservableHelper<TransmissionMessage>();

        #endregion Fields
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-12 08:57
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class BasicApi
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public BasicApi()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##URL## ##Username## ##ActionUsername## ##Password## ##Provider##
        implement ToString URL=##URL##, Username=##Username##, ActionUsername=##ActionUsername##, Password=##Password##, Provider=##Provider##
        implement equals URL, Username, ActionUsername, Password, Provider
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="URL">URL bazowy cpanela</param>
        /// <param name="Username">Nazwa użytkownika logującego się</param>
        /// <param name="Password">hasło</param>
        /// </summary>
        public BasicApi(string URL, string Username, string Password)
        {
            this.URL = URL;
            this.Username = Username;
            this.Password = Password;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności URL; URL bazowy cpanela
        /// </summary>
        public const string PROPERTYNAME_URL = "URL";
        /// <summary>
        /// Nazwa własności Username; Nazwa użytkownika logującego się
        /// </summary>
        public const string PROPERTYNAME_USERNAME = "Username";
        /// <summary>
        /// Nazwa własności ActionUsername; Nazwa użytkownika, w konteście którego wykonywania jest akcja
        /// </summary>
        public const string PROPERTYNAME_ACTIONUSERNAME = "ActionUsername";
        /// <summary>
        /// Nazwa własności Password; hasło
        /// </summary>
        public const string PROPERTYNAME_PASSWORD = "Password";
        /// <summary>
        /// Nazwa własności Provider; opcjonalna informacja o dostawcy usług hostingowych
        /// </summary>
        public const string PROPERTYNAME_PROVIDER = "Provider";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// URL bazowy cpanela
        /// </summary>
        public string URL
        {
            get
            {
                return uRL;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == uRL) return;
                uRL = value;
                NotifyPropertyChanged(PROPERTYNAME_URL);
            }
        }
        private string uRL = string.Empty;
        /// <summary>
        /// Nazwa użytkownika logującego się
        /// </summary>
        public string Username
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
        private string username = string.Empty;
        /// <summary>
        /// Nazwa użytkownika, w konteście którego wykonywania jest akcja
        /// </summary>
        public string ActionUsername
        {
            get
            {
                return actionUsername;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == actionUsername) return;
                actionUsername = value;
                NotifyPropertyChanged(PROPERTYNAME_ACTIONUSERNAME);
            }
        }
        private string actionUsername = string.Empty;
        /// <summary>
        /// hasło
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == password) return;
                password = value;
                NotifyPropertyChanged(PROPERTYNAME_PASSWORD);
            }
        }
        private string password = string.Empty;
        /// <summary>
        /// opcjonalna informacja o dostawcy usług hostingowych
        /// </summary>
        public HostingProvider Provider
        {
            get
            {
                return provider;
            }
            set
            {
                if (value == provider) return;
                provider = value;
                NotifyPropertyChanged(PROPERTYNAME_PROVIDER);
            }
        }
        private HostingProvider provider;
        #endregion Properties
    }
}
