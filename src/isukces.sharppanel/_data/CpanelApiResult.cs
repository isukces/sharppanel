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
    implement Constructor OriginalResult
    implement INotifyPropertyChanged_Passive
    
    property OriginalResult XDocument Oryginalny dokument
    	OnChange Update();
    
    property ErrorMessage string komunikat błędu
    
    property Reason string Opis sytuacji
    
    property Success bool czy operacja wykonała się poprawnie
    
    property Function string nazwa wywołanej funkcji
    
    property Module string nazwa modułu
    
    property ApiVersion int wersja API
    
    property XData IEnumerable<XElement> Gałęzie DATA
    	read only OriginalResult == null ? new XElement[0] : OriginalResult.Descendants("data")
     * 
    smartClassEnd
    */

    public partial class CpanelApiResult : NotifyPropertyChangedBase
    {

        private static string Get(XElement e, string n)
        {
            var tt = e.Element(n);
            if (tt == null) return string.Empty;
            return tt.Value;
        }

        /// <summary>
        /// Rzutuje gałęzie data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public T[] GetResult<T>(Func<XElement, T> selector)
        {
            return XData.Select(selector).ToArray();
        }

        public XElement GetFirstDataElement(string name)
        {
            var xd = XData;
            if (xd == null || !xd.Any()) return null;
            return xd.First().Element(name);
        }


        public void Update()
        {
            if (originalResult == null) return;
            var cpanelresult = originalResult.Root;
            if (cpanelresult.Name != "cpanelresult") return;

            var apiversion = Get(cpanelresult, "apiversion");
            if (!string.IsNullOrEmpty(apiversion))
                ApiVersion = int.Parse(apiversion);
            ErrorMessage = Get(cpanelresult, "error");
            Function = Get(cpanelresult, "func");
            Module = Get(cpanelresult, "module");

            var data = cpanelresult.Element("data");
            if (data != null)
            {
                var result = Get(data, "result");
                Success = result == "1";
                Reason = Get(data, "reason");
            }
            /*
            <cpanelresult>
              <apiversion>2</apiversion>
  <data>
    <reason>niewiem.you.yourprovider.com domainadmin-domainexistsglobal</reason>
    <result>0</result>
  </data>
  <error>niewiem.you.yourprovider.com domainadmin-domainexistsglobal</error>
  <event>
    <result>1</result>
  </event>
  <func>addaddondomain</func>
  <module>AddonDomain</module>
</cpanelresult>
             */
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-08 16:25
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class CpanelApiResult
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public CpanelApiResult()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##OriginalResult## ##ErrorMessage## ##Reason## ##Success## ##Function## ##Module## ##ApiVersion## ##XData##
        implement ToString OriginalResult=##OriginalResult##, ErrorMessage=##ErrorMessage##, Reason=##Reason##, Success=##Success##, Function=##Function##, Module=##Module##, ApiVersion=##ApiVersion##, XData=##XData##
        implement equals OriginalResult, ErrorMessage, Reason, Success, Function, Module, ApiVersion, XData
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="OriginalResult">Oryginalny dokument</param>
        /// </summary>
        public CpanelApiResult(XDocument OriginalResult)
        {
            this.OriginalResult = OriginalResult;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności OriginalResult; Oryginalny dokument
        /// </summary>
        public const string PROPERTYNAME_ORIGINALRESULT = "OriginalResult";
        /// <summary>
        /// Nazwa własności ErrorMessage; komunikat błędu
        /// </summary>
        public const string PROPERTYNAME_ERRORMESSAGE = "ErrorMessage";
        /// <summary>
        /// Nazwa własności Reason; Opis sytuacji
        /// </summary>
        public const string PROPERTYNAME_REASON = "Reason";
        /// <summary>
        /// Nazwa własności Success; czy operacja wykonała się poprawnie
        /// </summary>
        public const string PROPERTYNAME_SUCCESS = "Success";
        /// <summary>
        /// Nazwa własności Function; nazwa wywołanej funkcji
        /// </summary>
        public const string PROPERTYNAME_FUNCTION = "Function";
        /// <summary>
        /// Nazwa własności Module; nazwa modułu
        /// </summary>
        public const string PROPERTYNAME_MODULE = "Module";
        /// <summary>
        /// Nazwa własności ApiVersion; wersja API
        /// </summary>
        public const string PROPERTYNAME_APIVERSION = "ApiVersion";
        /// <summary>
        /// Nazwa własności XData; Gałęzie DATA
        /// </summary>
        public const string PROPERTYNAME_XDATA = "XData";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Oryginalny dokument
        /// </summary>
        public XDocument OriginalResult
        {
            get
            {
                return originalResult;
            }
            set
            {
                if (value == originalResult) return;
                originalResult = value;
                Update();
                NotifyPropertyChanged(PROPERTYNAME_ORIGINALRESULT);
            }
        }
        private XDocument originalResult;
        /// <summary>
        /// komunikat błędu
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == errorMessage) return;
                errorMessage = value;
                NotifyPropertyChanged(PROPERTYNAME_ERRORMESSAGE);
            }
        }
        private string errorMessage = string.Empty;
        /// <summary>
        /// Opis sytuacji
        /// </summary>
        public string Reason
        {
            get
            {
                return reason;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == reason) return;
                reason = value;
                NotifyPropertyChanged(PROPERTYNAME_REASON);
            }
        }
        private string reason = string.Empty;
        /// <summary>
        /// czy operacja wykonała się poprawnie
        /// </summary>
        public bool Success
        {
            get
            {
                return success;
            }
            set
            {
                if (value == success) return;
                success = value;
                NotifyPropertyChanged(PROPERTYNAME_SUCCESS);
            }
        }
        private bool success;
        /// <summary>
        /// nazwa wywołanej funkcji
        /// </summary>
        public string Function
        {
            get
            {
                return function;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == function) return;
                function = value;
                NotifyPropertyChanged(PROPERTYNAME_FUNCTION);
            }
        }
        private string function = string.Empty;
        /// <summary>
        /// nazwa modułu
        /// </summary>
        public string Module
        {
            get
            {
                return module;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                if (value == module) return;
                module = value;
                NotifyPropertyChanged(PROPERTYNAME_MODULE);
            }
        }
        private string module = string.Empty;
        /// <summary>
        /// wersja API
        /// </summary>
        public int ApiVersion
        {
            get
            {
                return apiVersion;
            }
            set
            {
                if (value == apiVersion) return;
                apiVersion = value;
                NotifyPropertyChanged(PROPERTYNAME_APIVERSION);
            }
        }
        private int apiVersion;
        /// <summary>
        /// Gałęzie DATA; własność jest tylko do odczytu.
        /// </summary>
        public IEnumerable<XElement> XData
        {
            get
            {
                return OriginalResult == null ? new XElement[0] : OriginalResult.Descendants("data");
            }
        }
        #endregion Properties

    }
}
