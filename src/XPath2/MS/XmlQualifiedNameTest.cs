//     
//      Copyright (c) 2006 Microsoft Corporation.  All rights reserved.
//     
//      The use and distribution terms for this software are contained in the file
//      named license.txt, which can be found in the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by the
//      terms of this license.
//     
//      You must not remove this notice, or any other, from this software.
//     

using System.Xml;

namespace Wmhelp.XPath2.MS
{
    /// <summary>
    /// XmlQualifiedName extends XmlQualifiedName to support wildcards and adds nametest functionality
    /// Following are the examples:
    ///     {A}:B     XmlQualifiedNameTest.New("B", "A")        Match QName with namespace A        and local name B
    ///     *         XmlQualifiedNameTest.New(null, null)      Match any QName
    ///     {A}:*     XmlQualifiedNameTest.New(null, "A")       Match QName with namespace A        and any local name
    ///               XmlQualifiedNameTest.New("A", false)
    ///     *:B       XmlQualifiedNameTest.New("B", null)       Match QName with any namespace      and local name B
    ///     ~{A}:*    XmlQualifiedNameTest.New("B", "A")        Match QName with namespace not A    and any local name
    ///     {~A}:B    only as a result of the intersection      Match QName with namespace not A    and local name B
    /// </summary>
    public class XmlQualifiedNameTest : XmlQualifiedName
    {
        private readonly bool exclude;
        private const string wildcard = "*";
        private static readonly XmlQualifiedNameTest wc = New(wildcard, wildcard);

        /// <summary>
        /// Full wildcard
        /// </summary>
        public static XmlQualifiedNameTest Wildcard => wc;

        /// <summary>
        /// Constructor
        /// </summary>
        private XmlQualifiedNameTest(string name, string ns, bool exclude)
            : base(name, ns)
        {
            this.exclude = exclude;
        }

        /// <summary>
        /// Construct new from name and namespace. Returns singleton Wildcard in case full wildcard
        /// </summary>
        public static XmlQualifiedNameTest New(string name, string ns)
        {
            if (ns == null && name == null)
            {
                return Wildcard;
            }
            else
            {
                return new XmlQualifiedNameTest(name == null ? wildcard : name, ns == null ? wildcard : ns, false);
            }
        }

        public static XmlQualifiedNameTest New(XmlQualifiedName qn)
        {
            if (qn.IsEmpty)
                return Wildcard;
            else
                return new XmlQualifiedNameTest(qn.Name == null ? wildcard : qn.Name,
                    qn.Namespace == null ? wildcard : qn.Namespace, false);
        }

        /// <summary>
        /// True if matches any name and any namespace
        /// </summary>
        public bool IsWildcard => (object) this == (object) Wildcard;

        /// <summary>
        /// True if matches any name
        /// </summary>
        public bool IsNameWildcard => (object) Name == (object) wildcard;

        /// <summary>
        /// True if matches any namespace
        /// </summary>
        public bool IsNamespaceWildcard => (object) Namespace == (object) wildcard;

        public bool IsExclude => exclude;

        private bool IsNameSubsetOf(XmlQualifiedNameTest other)
        {
            return other.IsNameWildcard || Name == other.Name;
        }

        private bool IsNamespaceSubsetOf(XmlQualifiedNameTest other)
        {
            return other.IsNamespaceWildcard
                   || (exclude == other.exclude && Namespace == other.Namespace)
                   || (other.exclude && !exclude && Namespace != other.Namespace);
        }

        /// <summary>
        /// True if this matches every QName other does
        /// </summary>
        public bool IsSubsetOf(XmlQualifiedNameTest other)
        {
            return IsNameSubsetOf(other) && IsNamespaceSubsetOf(other);
        }

        /// <summary>
        /// Return true if the result of intersection with other is not empty
        /// </summary>
        public bool HasIntersection(XmlQualifiedNameTest other)
        {
            return (IsNamespaceSubsetOf(other) || other.IsNamespaceSubsetOf(this)) &&
                   (IsNameSubsetOf(other) || other.IsNameSubsetOf(this));
        }

        /// <summary>
        /// String representation
        /// </summary>
        public override string ToString()
        {
            if ((object) this == (object) Wildcard)
            {
                return "*";
            }
            else
            {
                if (Namespace.Length == 0)
                {
                    return Name;
                }
                else if ((object) Namespace == (object) wildcard)
                {
                    return "*:" + Name;
                }
                else if (exclude)
                {
                    return "{~" + Namespace + "}:" + Name;
                }
                else
                {
                    return "{" + Namespace + "}:" + Name;
                }
            }
        }
    }
}