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
    implement ToString ##Name##
    
    property Name string nazwa bazy danych
    
    property Size long wielkość
    
    property Users List<MysqlUser> Użytkownicy
    	init #
    	read only
    smartClassEnd
    */

    public partial class MysqlDB : NotifyPropertyChangedBase
    {
        public static MysqlDB FromXElement(XElement x)
        {
            MysqlDB mysqlDB = new MysqlDB()
            {
                Name = x.Element("db").Value,
                Size = int.Parse(x.Element("size").Value)
            };
            var users = x.Descendants("user").Select(q => new MysqlUser(q.Value));
            mysqlDB.users.AddRange(users);
            return mysqlDB;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-06 10:51
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class MysqlDB
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public MysqlDB()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##Size## ##Users##
        implement ToString Name=##Name##, Size=##Size##, Users=##Users##
        implement equals Name, Size, Users
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Name; nazwa bazy danych
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności Size; wielkość
        /// </summary>
        public const string PROPERTYNAME_SIZE = "Size";
        /// <summary>
        /// Nazwa własności Users; Użytkownicy
        /// </summary>
        public const string PROPERTYNAME_USERS = "Users";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", name);
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// nazwa bazy danych
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
        /// wielkość
        /// </summary>
        public long Size
        {
            get
            {
                return size;
            }
            set
            {
                if (value == size) return;
                size = value;
                NotifyPropertyChanged(PROPERTYNAME_SIZE);
            }
        }
        private long size;
        /// <summary>
        /// Użytkownicy; własność jest tylko do odczytu.
        /// </summary>
        public List<MysqlUser> Users
        {
            get
            {
                return users;
            }
        }
        private List<MysqlUser> users = new List<MysqlUser>();
        #endregion Properties

    }
}
