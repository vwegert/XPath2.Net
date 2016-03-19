// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

using System.Xml.XPath;

namespace Wmhelp.XPath2
{
    public class NodeProvider : IContextProvider
    {
        private readonly XPathItem item;

        public NodeProvider(XPathItem item)
        {
            this.item = item;
        }

        #region IContextProvider Members

        public XPathItem Context => item;

        public int CurrentPosition => 1;

        public int LastPosition => 1;

        #endregion
    }
}