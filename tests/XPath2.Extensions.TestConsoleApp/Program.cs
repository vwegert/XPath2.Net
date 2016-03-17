using System;
using System.Xml;
using System.Xml.XPath;
using Wmhelp.XPath2;
using Wmhelp.XPath2.Extensions;

namespace XPath2.Extensions.TestConsoleApp
{
    public class Program
    {
        private static void Exec(XPathNavigator navigator, string function)
        {
            Console.WriteLine("{0} = {1}", function, navigator.XPath2Evaluate(function));
        }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            FunctionTable.Inst.AddAllExtensions();

            var doc = new XmlDocument();
            var navigator = doc.CreateNavigator();

            Exec(navigator, "substring(null, 2)");

            Exec(navigator, "concat('a', 'z')");

            Exec(navigator, "generate-id()");

            try
            {
                navigator.XPath2Evaluate("base64encode('stef', 'x')");
            }
            catch (Exception ex)
            {
                Console.WriteLine("base64encode('stef', 'x') = " + ex.Message);
            }

            Exec(navigator, "base64encode('stef')");
            Exec(navigator, "base64encode('stef', 'ascii')");

            string encoded = (string) navigator.XPath2Evaluate("base64encode('stef-τίποτα', 'utf-8')");
            Console.WriteLine("base64encode('stef-τίποτα', 'utf-8') = " + encoded);
            Console.WriteLine("base64decode('{0}', 'utf-8') = {1}", encoded, navigator.XPath2Evaluate($"base64decode('{encoded}')"));
        }
    }
}