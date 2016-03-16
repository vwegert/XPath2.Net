using System;
using System.Text;
using Wmhelp.XPath2.MS;

namespace Wmhelp.XPath2.Extensions
{
    public static class FunctionTableExtensions
    {
        /// <summary>
        /// Extend the XPath2 FunctionTable with:
        /// - generate-id
        /// - base64encode
        /// - base64decode
        /// </summary>
        /// <param name="functionTable">The function table.</param>
        public static void AddAllExtensions(this FunctionTable functionTable)
        {
            functionTable.AddGenerateId();
            functionTable.AddBase64Encode();
            functionTable.AddBase64Decode();
        }

        /// <summary>
        /// Extend the XPath2 FunctionTable with 'generate-id' function to generate a Guid (http://www.w3schools.com/xsl/func_generateid.asp)
        /// </summary>
        /// <param name="functionTable">The function table.</param>
        public static void AddGenerateId(this FunctionTable functionTable)
        {
            functionTable.Add(XmlReservedNs.NsXQueryFunc, "generate-id", 0, XPath2ResultType.String, (context, provider, args) => Guid.NewGuid().ToString().ToLower());
        }

        /// <summary>
        /// Extend the XPath2 FunctionTable with 'base64encode' function to encode a string to base64 string.
        /// </summary>
        /// <param name="functionTable">The function table.</param>
        public static void AddBase64Encode(this FunctionTable functionTable)
        {
            XPathFunctionDelegate base64EncodeDelegate = (context, provider, args) =>
            {
                string value = CoreFuncs.CastToStringExactOne(context, args[0]);
                Encoding encoding = ParseEncodingFromArgs(context, args);

                return Convert.ToBase64String(encoding.GetBytes(value));
            };

            // base64encode with default UTF-8 encoding
            functionTable.Add(XmlReservedNs.NsXQueryFunc, "base64encode", 1, XPath2ResultType.String, (context, provider, args) => base64EncodeDelegate(context, provider, args));

            // base64encode with specified encoding
            functionTable.Add(XmlReservedNs.NsXQueryFunc, "base64encode", 2, XPath2ResultType.String, (context, provider, args) => base64EncodeDelegate(context, provider, args));
        }

        /// <summary>
        /// Extend the XPath2 FunctionTable with 'base64decode' function to decode base64 string to a string.
        /// </summary>
        /// <param name="functionTable">The function table.</param>
        public static void AddBase64Decode(this FunctionTable functionTable)
        {
            XPathFunctionDelegate base64DecodeDelegate = (context, provider, args) =>
            {
                string value = CoreFuncs.CastToStringExactOne(context, args[0]);
                Encoding encoding = ParseEncodingFromArgs(context, args);

                return encoding.GetString(Convert.FromBase64String(value));
            };

            // base64decode with default UTF-8 encoding
            functionTable.Add(XmlReservedNs.NsXQueryFunc, "base64decode", 1, XPath2ResultType.String, (context, provider, args) => base64DecodeDelegate(context, provider, args));

            // base64decode with specified encoding
            functionTable.Add(XmlReservedNs.NsXQueryFunc, "base64decode", 2, XPath2ResultType.String, (context, provider, args) => base64DecodeDelegate(context, provider, args));
        }

        #region private helper methods
        private static Encoding ParseEncodingFromArgs(XPath2Context context, object[] args)
        {
            string name = args.Length == 2 ? (string) CoreFuncs.CastToStringOptional(context, args[1]) : "UTF-8";

            try
            {
                return Encoding.GetEncoding(name);
            }
            catch (Exception)
            {
                throw new XPath2Exception("FORG0001", Properties.Resources.FORG0001, name, "Encoding.GetEncoding()");
            }
        }
        #endregion
    }
}