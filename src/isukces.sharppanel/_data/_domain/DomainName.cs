using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Name
    implement INotifyPropertyChanged_Passive
    implement ToString ##Name##
    implement Equals Name
    
    property Name string nazwa domeny
    	OnChange Update();
    
    property NameItems string[] Elementy nazwy
    	read only
    
    property Segments int Ilość segmentów nazwy
    	read only
    smartClassEnd
    */

    public partial class DomainName : NotifyPropertyChangedBase
    {
        #region Static Methods

        // Public Methods 

        public static implicit operator DomainName(string a)
        {
            return new DomainName(a);
        }

        public static implicit operator string(DomainName a)
        {
            return a.name;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public IEnumerable<string> GetParents(int min, int max)
        {
            max = Math.Min(max, segments);
            min = Math.Max(min, 1);
            for (int itemsInName = max; itemsInName >= min; itemsInName--)
            {
                if (itemsInName == segments)
                    yield return name;
                else
                {
                    var parentItems = new string[itemsInName];
                    Array.Copy(nameItems, nameItems.Length - itemsInName, parentItems, 0, itemsInName);
                    string parent = string.Join(".", parentItems);
                    yield return parent;
                }
            }
        }

        public string GetURL(bool addWWWPrefix)
        {
            string x = GetWithoutWWW();
            if (addWWWPrefix)
                return string.Format("http://www.{0}/", x);
            else
                return string.Format("http://{0}/", x);
        }

        public DomainName GetWithoutWWW()
        {
            if (name.ToLower().StartsWith("www."))
                return name.Substring(4);
            return name;
        }

        public bool nameEquals(string x)
        {
            return name.ToLower() == (x ?? "").ToLower();
        }

        public bool nameEquals(DomainName x)
        {
            x = x ?? new DomainName("");
            return name.ToLower() == x.name.ToLower();
        }
        // Private Methods 

        private void Update()
        {
            nameItems = name.Split('.');
            segments = nameItems.Length;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-08 19:32
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class DomainName : IEquatable<DomainName>
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public DomainName()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##NameItems## ##Segments##
        implement ToString Name=##Name##, NameItems=##NameItems##, Segments=##Segments##
        implement equals Name, NameItems, Segments
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name">nazwa domeny</param>
        /// </summary>
        public DomainName(string Name)
        {
            this.Name = Name;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Name; nazwa domeny
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności NameItems; Elementy nazwy
        /// </summary>
        public const string PROPERTYNAME_NAMEITEMS = "NameItems";
        /// <summary>
        /// Nazwa własności Segments; Ilość segmentów nazwy
        /// </summary>
        public const string PROPERTYNAME_SEGMENTS = "Segments";
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

        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(DomainName other)
        {
            return other == this;
        }

        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public override bool Equals(object other)
        {
            if (!(other is DomainName)) return false;
            return Equals((DomainName)other);
        }

        /// <summary>
        /// Zwraca kod HASH obiektu
        /// </summary>
        /// <returns>kod HASH obiektu</returns>
        public override int GetHashCode()
        {
            // Good implementation suggested by Josh Bloch
            int _hash_ = 17;
            _hash_ = _hash_ * 31 + name.GetHashCode();
            return _hash_;
        }

        #endregion Methods

        #region Operators
        /// <summary>
        /// Realizuje operator ==
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są równe</returns>
        public static bool operator ==(DomainName left, DomainName right)
        {
            if (left == (object)null && right == (object)null) return true;
            if (left == (object)null || right == (object)null) return false;
            return left.name == right.name;
        }

        /// <summary>
        /// Realizuje operator !=
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są różne</returns>
        public static bool operator !=(DomainName left, DomainName right)
        {
            if (left == (object)null && right == (object)null) return false;
            if (left == (object)null || right == (object)null) return true;
            return left.name != right.name;
        }

        #endregion Operators

        #region Properties
        /// <summary>
        /// nazwa domeny
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
                Update();
                NotifyPropertyChanged(PROPERTYNAME_NAME);
            }
        }
        private string name = string.Empty;
        /// <summary>
        /// Elementy nazwy; własność jest tylko do odczytu.
        /// </summary>
        public string[] NameItems
        {
            get
            {
                return nameItems;
            }
        }
        private string[] nameItems;
        /// <summary>
        /// Ilość segmentów nazwy; własność jest tylko do odczytu.
        /// </summary>
        public int Segments
        {
            get
            {
                return segments;
            }
        }
        private int segments;
        #endregion Properties

    }
}
