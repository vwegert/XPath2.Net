// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

namespace Wmhelp.XPath2.AST
{
    /// <summary>
    /// An AST node that represents a discrete value.
    /// This class is used by XPath.Net internally. It isn't intended for use in application code.
    /// </summary>
    public sealed class ValueNode : AbstractNode
    {
        private object _value;

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

        /// <summary>
        /// Changes the value represented by this AST node.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(object value)
        {
            _value = value;
        }

        public override string Render()
        {
            if (_value is string)
            {
                // TODO implement more sophisticated escaping / quote handling
                return "'" + _value + "'";
            }
            else
            {
                return _value.ToString();
            }
        }

    }
}