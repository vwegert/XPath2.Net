using System;
using System.Xml;
using Wmhelp.XPath2;
using Wmhelp.XPath2.Extensions;

namespace XPath2.Extensions.TestConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            FunctionTable.Inst.AddAllExtensions();

            var doc = new XmlDocument();
            var navigator = doc.CreateNavigator();

            Console.WriteLine("concat('a', 'z') = " + navigator.XPath2Evaluate("concat('a', 'z')"));

            Console.WriteLine("generate-id() = " + navigator.XPath2Evaluate("generate-id()"));

            try
            {
                navigator.XPath2Evaluate("base64encode('stef', 'x')");
            }
            catch (Exception ex)
            {
                Console.WriteLine("base64encode('stef', 'x') = " + ex.Message);
            }

            Console.WriteLine("base64encode('stef') = " + navigator.XPath2Evaluate("base64encode('stef')"));
            Console.WriteLine("base64encode('stef', 'ascii') = " + navigator.XPath2Evaluate("base64encode('stef', 'ascii')"));

            string encoded = (string) navigator.XPath2Evaluate("base64encode('stef-τίποτα', 'utf-8')");
            Console.WriteLine("base64encode('stef-τίποτα', 'utf-8') = " + encoded);

            Console.WriteLine("base64decode('{0}', 'utf-8') = {1}", encoded, navigator.XPath2Evaluate($"base64decode('{encoded}')"));
        }
    }
}