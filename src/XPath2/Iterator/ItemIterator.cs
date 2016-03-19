// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

using System.Xml.XPath;
using Wmhelp.XPath2.Properties;

namespace Wmhelp.XPath2.Iterator
{
    internal class ItemIterator : XPath2NodeIterator
    {
        private XPath2NodeIterator iter;

        public ItemIterator(XPath2NodeIterator baseIter)
        {
            iter = baseIter;
        }

        public override XPath2NodeIterator Clone()
        {
            return new ItemIterator(iter.Clone());
        }

        public override XPath2NodeIterator CreateBufferedIterator()
        {
            return new BufferedNodeIterator(Clone());
        }

        protected override XPathItem NextItem()
        {
            if (CurrentPosition == -1 && iter.IsStarted)
                return iter.Current;
            if (iter.MoveNext())
            {
                if (iter.Current.IsNode)
                    throw new XPath2Exception(Resources.XPTY0018, "");
                return iter.Current;
            }
            return null;
        }
    }
}