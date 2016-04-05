using System;
using System.Xml;
using System.Xml.XPath;
using Wmhelp.XPath2;
using Wmhelp.XPath2.Extensions;

namespace XPath2.Extensions.Tests
{
    public class XPath2TestFixture : IDisposable
    {
        public readonly XPathNavigator Navigator;

        public XPath2TestFixture()
        {
            var doc = new XmlDocument();
            Navigator = doc.CreateNavigator();

            FunctionTable.Inst.AddAllExtensions();
        }

        public void Dispose()
        {
        }
    }
}