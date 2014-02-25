using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Linq;

namespace isukces.sharppanel
{
    /*
    smartClass
    option NoAdditionalFile
    implement INotifyPropertyChanged
    smartClassEnd
    */

    public partial class NotifyPropertyChangedBase
    {

        //static readonly string[] URLENCODE_DST = new string[] { "%25", "%20", "%7E", "%21", "%40", "%23", "%24", "%5E", "%26", "%2A", "%28", "%29", "%7B", "%7D", "%5B", "%5D", "%3D", "%3A", "%2F", "%2C", "%3B", "%3F", "%2B", "%27", "%22", "%5C" };
        //static readonly char[] URLENCODE_SRC = new char[] { '%', ' ', '~', '!', '@', '#', '$', '^', '&', '*', '(', ')', '{', '}', '[', ']', '=', ':', '/', ',', (char)59 /* średnik */, '?', '+', '\'', '"', '\\' };

        internal static int getInt(XElement x)
        {
            if (x == null) return 0;
            if (x.Value == "unlimited") return 0;
            return int.Parse(x.Value);
        }
        internal static string getString(CpanelApiResult x, string name)
        {
            var d = x.XData.Where(q => q.Element("name").Value == name).FirstOrDefault();
            if (d != null)
                return d.Element("value").Value;
            return "";
        }

        internal static double getDouble(XElement x)
        {
            if (x == null) return 0;
            return parseDouble(x.Value);
        }

        internal static double parseDouble(string x)
        {
            x = x.Trim().ToLower();
            if (x == "unlimited") return double.PositiveInfinity;
            if (x == "none") return 0;
            return double.Parse(x, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        }

        ///// <summary>
        ///// Ręcznie zrobione URLEncode
        ///// </summary>
        ///// <param name="x"></param>
        ///// <returns></returns>
        //internal static string URLEncode2(string x)
        //{
        //    x = x ?? "";
        //    string src1 = new string(URLENCODE_SRC);
        //    StringBuilder sb = new StringBuilder();
        //    foreach (char c in x)
        //    {
        //        int i = src1.IndexOf(c);
        //        if (i < 0)
        //            sb.Append(c);
        //        else
        //            sb.Append(URLENCODE_DST[i]);
        //    }
        //    return sb.ToString();
        //}


        #region Static Methods

        // Protected Methods 

        protected static string RandomStr(int l)
        {
            string result = "";
            while (result.Length < l)
            {
                byte[] rr = new byte[128];
                RND.NextBytes(rr);
                result += Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("/", "a").Replace("\\", "a").Replace("+", "");
            }
            return result.Substring(0, l);
        }

        protected static string TmpDir(string file = null)
        {
            if (i == null)
            {
                i = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "WPInstant", RandomStr(12)));
                i.Create();
            }
            if (string.IsNullOrEmpty(file))
                return i.FullName;
            return Path.Combine(i.FullName, file);
        }


        private static DirectoryInfo i;

        public static void CleanUp()
        {
            if (i == null) return;
            try
            {
                i.Delete(true);
                i = null;
            }
            catch { }
        }

        #endregion Static Methods

        #region Methods

        // Protected Methods 



        #endregion Methods

        #region Static Fields

        protected static Random RND = new Random();

        #endregion Static Fields
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2012-07-03 10:58
// File generated automatically ver 2012-05-05 12:00
// SmartClass2010Net35, Version=1.0.1.21, Culture=neutral, PublicKeyToken=4aa5e0e1662c5135
namespace isukces.sharppanel
{
    public partial class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public Base()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString 
        implement ToString 
        implement equals 
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region INotifyPropertyChanged
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion INotifyPropertyChanged


        #region Constants
        #endregion Constants


        #region Methods
        protected void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion Methods


        #region Properties
        #endregion Properties
    }
}
