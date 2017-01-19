using System.Xml;
using System.Xml.XPath;

namespace Wmhelp.XPath2
{
    public static class XmlNodeExtensions
    {
        public static XmlNodeList XPath2SelectNodes(this XmlNode node, string xpath)
        {
            return XPath2SelectNodes(node, xpath, null);
        }

        public static XmlNodeList XPath2SelectNodes(this XmlNode node, string xpath, IXmlNamespaceResolver nsmgr)
        {
            XPathNavigator nav = node.CreateNavigator();
            return new NodeList(nav.XPath2Select(xpath, nsmgr), node.OwnerDocument);
        }

        public static object XPath2Evaluate(this XmlNode node, string xpath)
        {
            return XPath2Evaluate(node, xpath, null);
        }

        public static object XPath2Evaluate(this XmlNode node, string xpath, object arg)
        {
            return XPath2Evaluate(node, xpath, null, arg);
        }

        public static object XPath2Evaluate(this XmlNode node, string xpath, IXmlNamespaceResolver nsmgr)
        {
            return XPath2Evaluate(node, xpath, nsmgr, null);
        }

        public static object XPath2Evaluate(this XmlNode node, string xpath, IXmlNamespaceResolver nsmgr, object arg)
        {
            XPathNavigator nav = node.CreateNavigator();
            return nav.XPath2Evaluate(xpath, nsmgr, arg);
        }

        public static XmlNode XPath2SelectSingleNode(this XmlNode node, string xquery)
        {
            return XPath2SelectSingleNode(node, xquery, null);
        }

        public static XmlNode XPath2SelectSingleNode(this XmlNode node, string xquery, IXmlNamespaceResolver nsmgr)
        {
            return XPath2SelectSingleNode(node, xquery, nsmgr, null);
        }

        public static XmlNode XPath2SelectSingleNode(this XmlNode node, string xquery, object arg)
        {
            return XPath2SelectSingleNode(node, xquery, null, arg);
        }

        public static XmlNode XPath2SelectSingleNode(this XmlNode node, string xquery, IXmlNamespaceResolver nsmgr, object arg)
        {
            XPathNavigator nav = node.CreateNavigator();
            XPath2NodeIterator iter = nav.XPath2Select(xquery, nsmgr, arg);
            if (iter.MoveNext() && iter.Current.IsNode)
                return NodeList.ToXmlNode((XPathNavigator)iter.Current);
            return null;
        }
    }
}