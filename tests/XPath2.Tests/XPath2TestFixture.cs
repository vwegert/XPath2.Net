using System;
using System.Xml;
using System.Xml.XPath;

namespace XPath2.Tests
{
    public class XPath2TestFixture : IDisposable
    {
        public readonly XPathNavigator Navigator;

        public XPath2TestFixture()
        {
            var doc = new XmlDocument();
            Navigator = doc.CreateNavigator();
        }

        public void Dispose()
        {
        }
    }
}