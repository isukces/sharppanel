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
    
    property CREATEROUTINE bool A boolean value that indicates whether or not a user can create a routine. '1' if the user has the permission. '0' if the user does not.property /CREATEROUTINE bool
    
    property DROP bool A boolean value that indicates whether or not a user can drop something from the database. '1' if the user has the permission. '0' if the user does not.property /DROP bool
    
    property DELETE bool A boolean value that indicates whether or not a user can delete a table. '1' if the user has the permission. '0' if the user does not.property /DELETE bool
    
    property REFERENCES bool A boolean value that indicates whether or not a user can reference a database or table. '1' if the user has the permission. '0' if the user does not.property /REFERENCES bool
    
    property UPDATE bool A boolean value that indicates whether or not a user can update a table. '1' if the user has the permission. '0' if the user does not.property /UPDATE bool
    
    property ALTER bool A boolean value that indicates whether or not a user can alter a table with the 'ALTER TABLE' command. '1' if the user has the permission. '0' if the user does not.property /ALTER bool
    
    property CREATETEMPORARYTABLES bool A boolean value that indicates whether or not a user can create temporary tables. '1' if the user has the permission. '0' if the user does not.property /CREATETEMPORARYTABLES bool
    
    property INDEX bool A boolean value that indicates whether or not a user can create or remove indexes for tables. '1' if the user has the permission. '0' if the user does not.property /INDEX bool
    
    property CREATE bool A boolean value that indicates whether or not a user can make a new database, table or index. '1' if the user has the permission. '0' if the user does not.property /CREATE bool
    
    property LOCKTABLES bool A boolean value that indicates whether or not a user can lock tables. '1' if the user has the permission. '0' if the user does not.property /LOCKTABLES bool
    
    property SELECT bool A boolean value that indicates whether or not a user can select tables. '1' if the user has the permission. '0' if the user does not.property /SELECT bool
    
    property INSERT bool A boolean value that indicates whether or not a user can create tables. '1' if the user has the permission. '0' if the user does not.property /INSERT bool
    
    property ALLPRIVILEGES bool Czy są wszytkie uprawnienia
    	read only CREATEROUTINE && DROP && DELETE && REFERENCES && UPDATE && ALTER && CREATETEMPORARYTABLES && INDEX && CREATE && LOCKTABLES && SELECT && INSERT
    smartClassEnd
    */

    public partial class MysqlUserdbprivs : NotifyPropertyChangedBase
    {
        #region Static Methods

        // Public Methods 

        public static MysqlUserdbprivs FromXElement(XElement x)
        {
            var tt = x.Element("ALLPRIVILEGES");
            if (tt != null && tt.Value == "1")
                return ALL;
            return new MysqlUserdbprivs()
            {
                CREATEROUTINE = x.Element("CREATEROUTINE").Value == "1",
                DROP = x.Element("DROP").Value == "1",
                DELETE = x.Element("DELETE").Value == "1",
                REFERENCES = x.Element("REFERENCES").Value == "1",
                UPDATE = x.Element("UPDATE").Value == "1",
                ALTER = x.Element("ALTER").Value == "1",
                CREATETEMPORARYTABLES = x.Element("CREATETEMPORARYTABLES").Value == "1",
                INDEX = x.Element("INDEX").Value == "1",
                CREATE = x.Element("CREATE").Value == "1",
                LOCKTABLES = x.Element("LOCKTABLES").Value == "1",
                SELECT = x.Element("SELECT").Value == "1",
                INSERT = x.Element("INSERT").Value == "1"
            };

        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public string GetAsString()
        {
            if (ALLPRIVILEGES) return "all";
            StringBuilder sb = new StringBuilder();
            if (ALTER) sb.Append("alter ");
            if (CREATETEMPORARYTABLES) sb.Append("temporary ");
            if (CREATEROUTINE) sb.Append("routine ");
            if (CREATE) sb.Append("create ");
            if (DELETE) sb.Append("delete ");
            if (DROP) sb.Append("drop ");
            if (SELECT) sb.Append("select ");
            if (INSERT) sb.Append("insert ");
            if (UPDATE) sb.Append("update ");

            if (REFERENCES) sb.Append("references ");
            if (INDEX) sb.Append("index ");
            if (LOCKTABLES) sb.Append("lock ");
            return sb.ToString().Trim();
        }

        #endregion Methods

        #region Static Properties

        public static MysqlUserdbprivs ALL
        {
            get
            {
                return new MysqlUserdbprivs()
                {
                    CREATEROUTINE = true,
                    DROP = true,
                    DELETE = true,
                    REFERENCES = true,
                    UPDATE = true,
                    ALTER = true,
                    CREATETEMPORARYTABLES = true,
                    INDEX = true,
                    CREATE = true,
                    LOCKTABLES = true,
                    SELECT = true,
                    INSERT = true
                };
            }
        }

        #endregion Static Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-06 11:18
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class MysqlUserdbprivs
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public MysqlUserdbprivs()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##CREATEROUTINE## ##DROP## ##DELETE## ##REFERENCES## ##UPDATE## ##ALTER## ##CREATETEMPORARYTABLES## ##INDEX## ##CREATE## ##LOCKTABLES## ##SELECT## ##INSERT## ##ALLPRIVILEGES##
        implement ToString CREATEROUTINE=##CREATEROUTINE##, DROP=##DROP##, DELETE=##DELETE##, REFERENCES=##REFERENCES##, UPDATE=##UPDATE##, ALTER=##ALTER##, CREATETEMPORARYTABLES=##CREATETEMPORARYTABLES##, INDEX=##INDEX##, CREATE=##CREATE##, LOCKTABLES=##LOCKTABLES##, SELECT=##SELECT##, INSERT=##INSERT##, ALLPRIVILEGES=##ALLPRIVILEGES##
        implement equals CREATEROUTINE, DROP, DELETE, REFERENCES, UPDATE, ALTER, CREATETEMPORARYTABLES, INDEX, CREATE, LOCKTABLES, SELECT, INSERT, ALLPRIVILEGES
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności CREATEROUTINE; A boolean value that indicates whether or not a user can create a routine. '1' if the user has the permission. '0' if the user does not.property /CREATEROUTINE bool
        /// </summary>
        public const string PROPERTYNAME_CREATEROUTINE = "CREATEROUTINE";
        /// <summary>
        /// Nazwa własności DROP; A boolean value that indicates whether or not a user can drop something from the database. '1' if the user has the permission. '0' if the user does not.property /DROP bool
        /// </summary>
        public const string PROPERTYNAME_DROP = "DROP";
        /// <summary>
        /// Nazwa własności DELETE; A boolean value that indicates whether or not a user can delete a table. '1' if the user has the permission. '0' if the user does not.property /DELETE bool
        /// </summary>
        public const string PROPERTYNAME_DELETE = "DELETE";
        /// <summary>
        /// Nazwa własności REFERENCES; A boolean value that indicates whether or not a user can reference a database or table. '1' if the user has the permission. '0' if the user does not.property /REFERENCES bool
        /// </summary>
        public const string PROPERTYNAME_REFERENCES = "REFERENCES";
        /// <summary>
        /// Nazwa własności UPDATE; A boolean value that indicates whether or not a user can update a table. '1' if the user has the permission. '0' if the user does not.property /UPDATE bool
        /// </summary>
        public const string PROPERTYNAME_UPDATE = "UPDATE";
        /// <summary>
        /// Nazwa własności ALTER; A boolean value that indicates whether or not a user can alter a table with the 'ALTER TABLE' command. '1' if the user has the permission. '0' if the user does not.property /ALTER bool
        /// </summary>
        public const string PROPERTYNAME_ALTER = "ALTER";
        /// <summary>
        /// Nazwa własności CREATETEMPORARYTABLES; A boolean value that indicates whether or not a user can create temporary tables. '1' if the user has the permission. '0' if the user does not.property /CREATETEMPORARYTABLES bool
        /// </summary>
        public const string PROPERTYNAME_CREATETEMPORARYTABLES = "CREATETEMPORARYTABLES";
        /// <summary>
        /// Nazwa własności INDEX; A boolean value that indicates whether or not a user can create or remove indexes for tables. '1' if the user has the permission. '0' if the user does not.property /INDEX bool
        /// </summary>
        public const string PROPERTYNAME_INDEX = "INDEX";
        /// <summary>
        /// Nazwa własności CREATE; A boolean value that indicates whether or not a user can make a new database, table or index. '1' if the user has the permission. '0' if the user does not.property /CREATE bool
        /// </summary>
        public const string PROPERTYNAME_CREATE = "CREATE";
        /// <summary>
        /// Nazwa własności LOCKTABLES; A boolean value that indicates whether or not a user can lock tables. '1' if the user has the permission. '0' if the user does not.property /LOCKTABLES bool
        /// </summary>
        public const string PROPERTYNAME_LOCKTABLES = "LOCKTABLES";
        /// <summary>
        /// Nazwa własności SELECT; A boolean value that indicates whether or not a user can select tables. '1' if the user has the permission. '0' if the user does not.property /SELECT bool
        /// </summary>
        public const string PROPERTYNAME_SELECT = "SELECT";
        /// <summary>
        /// Nazwa własności INSERT; A boolean value that indicates whether or not a user can create tables. '1' if the user has the permission. '0' if the user does not.property /INSERT bool
        /// </summary>
        public const string PROPERTYNAME_INSERT = "INSERT";
        /// <summary>
        /// Nazwa własności ALLPRIVILEGES; Czy są wszytkie uprawnienia
        /// </summary>
        public const string PROPERTYNAME_ALLPRIVILEGES = "ALLPRIVILEGES";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// A boolean value that indicates whether or not a user can create a routine. '1' if the user has the permission. '0' if the user does not.property /CREATEROUTINE bool
        /// </summary>
        public bool CREATEROUTINE
        {
            get
            {
                return cREATEROUTINE;
            }
            set
            {
                if (value == cREATEROUTINE) return;
                cREATEROUTINE = value;
                NotifyPropertyChanged(PROPERTYNAME_CREATEROUTINE);
            }
        }
        private bool cREATEROUTINE;
        /// <summary>
        /// A boolean value that indicates whether or not a user can drop something from the database. '1' if the user has the permission. '0' if the user does not.property /DROP bool
        /// </summary>
        public bool DROP
        {
            get
            {
                return dROP;
            }
            set
            {
                if (value == dROP) return;
                dROP = value;
                NotifyPropertyChanged(PROPERTYNAME_DROP);
            }
        }
        private bool dROP;
        /// <summary>
        /// A boolean value that indicates whether or not a user can delete a table. '1' if the user has the permission. '0' if the user does not.property /DELETE bool
        /// </summary>
        public bool DELETE
        {
            get
            {
                return dELETE;
            }
            set
            {
                if (value == dELETE) return;
                dELETE = value;
                NotifyPropertyChanged(PROPERTYNAME_DELETE);
            }
        }
        private bool dELETE;
        /// <summary>
        /// A boolean value that indicates whether or not a user can reference a database or table. '1' if the user has the permission. '0' if the user does not.property /REFERENCES bool
        /// </summary>
        public bool REFERENCES
        {
            get
            {
                return rEFERENCES;
            }
            set
            {
                if (value == rEFERENCES) return;
                rEFERENCES = value;
                NotifyPropertyChanged(PROPERTYNAME_REFERENCES);
            }
        }
        private bool rEFERENCES;
        /// <summary>
        /// A boolean value that indicates whether or not a user can update a table. '1' if the user has the permission. '0' if the user does not.property /UPDATE bool
        /// </summary>
        public bool UPDATE
        {
            get
            {
                return uPDATE;
            }
            set
            {
                if (value == uPDATE) return;
                uPDATE = value;
                NotifyPropertyChanged(PROPERTYNAME_UPDATE);
            }
        }
        private bool uPDATE;
        /// <summary>
        /// A boolean value that indicates whether or not a user can alter a table with the 'ALTER TABLE' command. '1' if the user has the permission. '0' if the user does not.property /ALTER bool
        /// </summary>
        public bool ALTER
        {
            get
            {
                return aLTER;
            }
            set
            {
                if (value == aLTER) return;
                aLTER = value;
                NotifyPropertyChanged(PROPERTYNAME_ALTER);
            }
        }
        private bool aLTER;
        /// <summary>
        /// A boolean value that indicates whether or not a user can create temporary tables. '1' if the user has the permission. '0' if the user does not.property /CREATETEMPORARYTABLES bool
        /// </summary>
        public bool CREATETEMPORARYTABLES
        {
            get
            {
                return cREATETEMPORARYTABLES;
            }
            set
            {
                if (value == cREATETEMPORARYTABLES) return;
                cREATETEMPORARYTABLES = value;
                NotifyPropertyChanged(PROPERTYNAME_CREATETEMPORARYTABLES);
            }
        }
        private bool cREATETEMPORARYTABLES;
        /// <summary>
        /// A boolean value that indicates whether or not a user can create or remove indexes for tables. '1' if the user has the permission. '0' if the user does not.property /INDEX bool
        /// </summary>
        public bool INDEX
        {
            get
            {
                return iNDEX;
            }
            set
            {
                if (value == iNDEX) return;
                iNDEX = value;
                NotifyPropertyChanged(PROPERTYNAME_INDEX);
            }
        }
        private bool iNDEX;
        /// <summary>
        /// A boolean value that indicates whether or not a user can make a new database, table or index. '1' if the user has the permission. '0' if the user does not.property /CREATE bool
        /// </summary>
        public bool CREATE
        {
            get
            {
                return cREATE;
            }
            set
            {
                if (value == cREATE) return;
                cREATE = value;
                NotifyPropertyChanged(PROPERTYNAME_CREATE);
            }
        }
        private bool cREATE;
        /// <summary>
        /// A boolean value that indicates whether or not a user can lock tables. '1' if the user has the permission. '0' if the user does not.property /LOCKTABLES bool
        /// </summary>
        public bool LOCKTABLES
        {
            get
            {
                return lOCKTABLES;
            }
            set
            {
                if (value == lOCKTABLES) return;
                lOCKTABLES = value;
                NotifyPropertyChanged(PROPERTYNAME_LOCKTABLES);
            }
        }
        private bool lOCKTABLES;
        /// <summary>
        /// A boolean value that indicates whether or not a user can select tables. '1' if the user has the permission. '0' if the user does not.property /SELECT bool
        /// </summary>
        public bool SELECT
        {
            get
            {
                return sELECT;
            }
            set
            {
                if (value == sELECT) return;
                sELECT = value;
                NotifyPropertyChanged(PROPERTYNAME_SELECT);
            }
        }
        private bool sELECT;
        /// <summary>
        /// A boolean value that indicates whether or not a user can create tables. '1' if the user has the permission. '0' if the user does not.property /INSERT bool
        /// </summary>
        public bool INSERT
        {
            get
            {
                return iNSERT;
            }
            set
            {
                if (value == iNSERT) return;
                iNSERT = value;
                NotifyPropertyChanged(PROPERTYNAME_INSERT);
            }
        }
        private bool iNSERT;
        /// <summary>
        /// Czy są wszytkie uprawnienia; własność jest tylko do odczytu.
        /// </summary>
        public bool ALLPRIVILEGES
        {
            get
            {
                return CREATEROUTINE && DROP && DELETE && REFERENCES && UPDATE && ALTER && CREATETEMPORARYTABLES && INDEX && CREATE && LOCKTABLES && SELECT && INSERT;
            }
        }
        #endregion Properties
    }
}
