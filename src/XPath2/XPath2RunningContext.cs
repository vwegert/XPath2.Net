// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

using System;
using System.Globalization;
using Wmhelp.XPath2.MS;
using Wmhelp.XPath2.Properties;

namespace Wmhelp.XPath2
{
    public class XPath2RunningContext
    {
        internal DateTime Now { get; }

        internal CultureInfo DefaultCulture { get; }

        internal string BaseUri { get; set; }

        internal bool IsOrdered { get; set; }

        internal NameBinder NameBinder { get; }

        public XPath2RunningContext()
        {
            Now = DateTime.Now;
            NameBinder = new NameBinder();

            DefaultCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            DefaultCulture.NumberFormat.CurrencyGroupSeparator = string.Empty;
            DefaultCulture.NumberFormat.NumberGroupSeparator = string.Empty;

            IsOrdered = true;
        }

        public CultureInfo GetCulture(string collationName)
        {
            if (string.IsNullOrEmpty(collationName) || collationName == XmlReservedNs.NsCollationCodepoint)
            {
                return null;
            }

            try
            {
                return CultureInfo.GetCultureInfoByIetfLanguageTag(collationName);
            }
            catch (ArgumentException)
            {
                throw new XPath2Exception("XQST0076", Resources.XQST0076, collationName);
            }
        }
    }
}