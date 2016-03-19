// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

namespace Wmhelp.XPath2.AST
{
    internal sealed class ValueNode : AbstractNode
    {
        private readonly object _value;

        public object Content => _value;

        public ValueNode(XPath2Context context, object value)
            : base(context)
        {
            _value = value;
        }

        public override object Execute(IContextProvider provider, object[] dataPool)
        {
            return _value;
        }

        public override XPath2ResultType GetReturnType(object[] dataPool)
        {
            return CoreFuncs.GetXPath2ResultType(_value);
        }

        public override bool IsEmptySequence()
        {
            return _value == Undefined.Value;
        }
    }
}