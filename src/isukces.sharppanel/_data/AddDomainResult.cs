using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace isukces.sharppanel
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor
    implement Constructor domain, status
    implement INotifyPropertyChanged
    
    property Domain RootDomain domena
    
    property Status AddDomainStatus status
    
    property IsNewDomain bool czy dodano nową domenę
    	read only status == AddDomainStatus.AddonDomainAdded || status == AddDomainStatus.SubDomainAdded;
    smartClassEnd
    */
    
    public partial class AddDomainResult
    {

    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-08-13 15:16
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class AddDomainResult : INotifyPropertyChanged
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AddDomainResult()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Domain## ##Status## ##IsNewDomain##
        implement ToString Domain=##Domain##, Status=##Status##, IsNewDomain=##IsNewDomain##
        implement equals Domain, Status, IsNewDomain
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AddDomainResult()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Domain">domena</param>
        /// <param name="Status">status</param>
        /// </summary>
        public AddDomainResult(RootDomain Domain, AddDomainStatus Status)
        {
            this.Domain = Domain;
            this.Status = Status;
        }

        #endregion Constructors

        #region INotifyPropertyChanged
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion INotifyPropertyChanged

        #region Constants
        /// <summary>
        /// Nazwa własności Domain; domena
        /// </summary>
        public const string PROPERTYNAME_DOMAIN = "Domain";
        /// <summary>
        /// Nazwa własności Status; status
        /// </summary>
        public const string PROPERTYNAME_STATUS = "Status";
        /// <summary>
        /// Nazwa własności IsNewDomain; czy dodano nową domenę
        /// </summary>
        public const string PROPERTYNAME_ISNEWDOMAIN = "IsNewDomain";
        #endregion Constants

        #region Methods
        protected void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion Methods

        #region Properties
        /// <summary>
        /// domena
        /// </summary>
        public RootDomain Domain
        {
            get
            {
                return domain;
            }
            set
            {
                if (value == domain) return;
                domain = value;
                NotifyPropertyChanged(PROPERTYNAME_DOMAIN);
            }
        }
        private RootDomain domain;
        /// <summary>
        /// status
        /// </summary>
        public AddDomainStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value == status) return;
                status = value;
                NotifyPropertyChanged(PROPERTYNAME_STATUS);
            }
        }
        private AddDomainStatus status;
        /// <summary>
        /// czy dodano nową domenę; własność jest tylko do odczytu.
        /// </summary>
        public bool IsNewDomain
        {
            get
            {
                return status == AddDomainStatus.AddonDomainAdded || status == AddDomainStatus.SubDomainAdded;;
            }
        }
        #endregion Properties

    }
}
