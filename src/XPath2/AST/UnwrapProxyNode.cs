// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

//using System;
//using System.Collections.Generic;
//using Wmhelp.XPath2.Proxy;

//namespace Wmhelp.XPath2.AST
//{
//    class UnwrapProxyNode: AbstractNode
//    {
//        public static object Unwrap(XPath2Context context, object expr)
//        {
//            if (expr is AtomizedUnaryOperatorNode || expr is AtomizedBinaryOperatorNode)
//                return new UnwrapProxyNode(context, expr);
//            return expr;
//        }

//        public UnwrapProxyNode(XPath2Context context, object node)
//            : base(context)
//        {
//            Add(node);
//        }

//        public override object Execute(IContextProvider provider)
//        {
//            object res = this[0].Execute(provider);
//            ValueProxy proxy = res as ValueProxy;
//            if (proxy != null)
//                return proxy.Value;
//            return res;
//        }
//    }
//}