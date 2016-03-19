// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.XPath;

namespace Wmhelp.XPath2
{
    public sealed class NodeIterator : XPath2NodeIterator
    {
        private readonly IEnumerable<XPathItem> master;
        private IEnumerator<XPathItem> iterator;

        public NodeIterator(IEnumerable<XPathItem> enumerable)
        {
            master = enumerable;
        }

        [DebuggerStepThrough]
        public override XPath2NodeIterator Clone()
        {
            return new NodeIterator(master);
        }

        public override XPath2NodeIterator CreateBufferedIterator()
        {
            return new BufferedNodeIterator(this);
        }

        protected override void Init()
        {
            iterator = master.GetEnumerator();
        }

        protected override XPathItem NextItem()
        {
            if (iterator.MoveNext())
                return iterator.Current;
            return null;
        }
    }
}