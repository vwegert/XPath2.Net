// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

using System;
using System.Collections;
using Wmhelp.XPath2.Proxy;

namespace Wmhelp.XPath2.AST
{
    public enum UnaryOperatorType
    {
        PLUS,   // used by AtomizedUnaryOperatorNode
        MINUS,  // used by AtomizedUnaryOperatorNode
        SOME,
        EVERY,
        INSTANCE_OF,
        TREAT_AS,
        CASTABLE,
        CAST_TO,
        CREATE,
        CAST_TO_ITEM
    }

    /// <summary>
    /// This delegate is used by XPath.Net internally. It isn't intended for use in application code.
    /// </summary>
    public delegate object UnaryOperator(IContextProvider provider, object arg);

    /// <summary>
    /// An AST node that represents a unary operator and its operand. 
    /// This class is used by XPath.Net internally. It isn't intended for use in application code.
    /// </summary>
    public class UnaryOperatorNode : AbstractNode
    {
        protected UnaryOperator _unaryOper;
        protected UnaryOperatorType _unaryOperatorType;
        protected SequenceType? _destType;
        protected bool _isString;
        private readonly XPath2ResultType _resultType;

        public UnaryOperatorNode(XPath2Context context, UnaryOperatorType operatorType, object node, XPath2ResultType resultType)
            : base(context)
        {
            _unaryOperatorType = operatorType;
            _unaryOper = DeriveOperatorFromType(operatorType);
            _resultType = resultType;
            Add(node);
        }

        public UnaryOperatorNode(XPath2Context context, UnaryOperatorType operatorType, SequenceType destType, object node, XPath2ResultType resultType)
            : base(context)
        {
            _unaryOperatorType = operatorType;
            _destType = destType;
            _unaryOper = DeriveOperatorFromType(operatorType);
            _resultType = resultType;
            Add(node);
        }

        public UnaryOperatorNode(XPath2Context context, UnaryOperatorType operatorType, SequenceType destType, bool isString, object node, XPath2ResultType resultType)
            : base(context)
        {
            _unaryOperatorType = operatorType;
            _destType = destType;
            _isString = isString;
            _unaryOper = DeriveOperatorFromType(operatorType);
            _resultType = resultType;
            Add(node);
        }


        private UnaryOperator DeriveOperatorFromType(UnaryOperatorType operatorType)
        {
            switch (operatorType)
            {
                case UnaryOperatorType.PLUS:
                    return (provider, arg) => 0 + ValueProxy.New(arg);
                case UnaryOperatorType.MINUS:
                    return (provider, arg) => - ValueProxy.New(arg);
                case UnaryOperatorType.SOME:
                    return (provider, arg) => CoreFuncs.Some(arg);
                case UnaryOperatorType.EVERY:
                    return (provider, arg) => CoreFuncs.Every(arg);
                case UnaryOperatorType.INSTANCE_OF:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to evaluate this operator without a destination type.");
                    return (provider, arg) => CoreFuncs.InstanceOf(Context, arg, _destType);
                case UnaryOperatorType.TREAT_AS:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to evaluate this operator without a destination type.");
                    return (provider, arg) => CoreFuncs.TreatAs(Context, arg, _destType);
                case UnaryOperatorType.CASTABLE:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to evaluate this operator without a destination type.");
                    return (provider, arg) => CoreFuncs.Castable(Context, arg, _destType, _isString);
                case UnaryOperatorType.CAST_TO:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to evaluate this operator without a destination type.");
                    return (provider, arg) => CoreFuncs.CastTo(Context, arg, _destType, _isString);
                case UnaryOperatorType.CREATE:
                    return (provider, arg) => XPath2NodeIterator.Create(CoreFuncs.GetRoot(arg));
                case UnaryOperatorType.CAST_TO_ITEM:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to evaluate this operator without a destination type.");
                    return (provider, arg) => CoreFuncs.CastToItem(Context, arg, _destType);
                default:
                    throw new NotImplementedException();
            }
        }

        public override object Execute(IContextProvider provider, object[] dataPool)
        {
            return _unaryOper(provider, this[0].Execute(provider, dataPool));
        }

        public override XPath2ResultType GetReturnType(object[] dataPool)
        {
            return _resultType;
        }

        /// <summary>
        /// Changes the operator represented by this AST node.
        /// Caution: Using this method might render the AST unusable for execution and evaluation,
        /// e. g. by changing the result type.
        /// </summary>
        /// <param name="value"></param>
        public void SetOperatorType(UnaryOperatorType operatorType)
        {
            _unaryOperatorType = operatorType;
            _unaryOper = DeriveOperatorFromType(operatorType);
        }

        /// <inheritdoc/>
        public override string Render()
        {
            switch (_unaryOperatorType)
            {
                case UnaryOperatorType.PLUS:
                    return "+" + this[0].Render();
                case UnaryOperatorType.MINUS:
                    return "-" + this[0].Render();
                case UnaryOperatorType.SOME:
                    return "some " + this[0].Render();
                case UnaryOperatorType.EVERY:
                    return "every " + this[0].Render();
                case UnaryOperatorType.INSTANCE_OF:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to render this operator without a destination type.");
                    return this[0].Render() + " instance of " + _destType.ToString();
                case UnaryOperatorType.TREAT_AS:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to render this operator without a destination type.");
                    return this[0].Render() + " treat as " + _destType.ToString();
                case UnaryOperatorType.CASTABLE:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to render this operator without a destination type.");
                    return this[0].Render() + " castable as " + _destType.ToString();
                case UnaryOperatorType.CAST_TO:
                    if (_destType == null)
                        throw new InvalidOperationException("Unable to render this operator without a destination type.");
                    return this[0].Render() + " cast as " + _destType.ToString();
                case UnaryOperatorType.CREATE:
                    return "/"; // only used by root PathExpr
                // TODO implement rendering of CAST_TO_ITEM node
                default:
                    throw new NotImplementedException();
            }
        }
    }
}