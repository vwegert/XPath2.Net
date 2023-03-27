// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

using System;
using Wmhelp.XPath2.Proxy;

namespace Wmhelp.XPath2.AST
{
    public enum BinaryOperatorType
    {
        ADD,            // used by ArithmeticBinaryOperatorNode
        SUBTRACT,       // used by ArithmeticBinaryOperatorNode
        MULTIPLY,       // used by ArithmeticBinaryOperatorNode
        DIVIDE,         // used by ArithmeticBinaryOperatorNode
        INT_DIVIDE,     // used by ArithmeticBinaryOperatorNode
        MODULO,         // used by ArithmeticBinaryOperatorNode
        VAL_COMP_EQ,    // used by AtomizedBinaryOperatorNode
        VAL_COMP_NE,    // used by AtomizedBinaryOperatorNode
        VAL_COMP_GT,    // used by AtomizedBinaryOperatorNode
        VAL_COMP_GE,    // used by AtomizedBinaryOperatorNode
        VAL_COMP_LT,    // used by AtomizedBinaryOperatorNode
        VAL_COMP_LE,    // used by AtomizedBinaryOperatorNode
        SAME_NODE,      // used by SingletonBinaryOperatorNode
        PRECEDING_NODE, // used by SingletonBinaryOperatorNode
        FOLLOWING_NODE, // used by SingletonBinaryOperatorNode
        UNION,          // used by OrderedBinaryOperatorNode
        UNION_OP,       // used by OrderedBinaryOperatorNode
        INTERSECT,      // used by OrderedBinaryOperatorNode
        GEN_COMP_EQ,    // used by BinaryOperatorNode
        GEN_COMP_NE,    // used by BinaryOperatorNode
        GEN_COMP_GT,    // used by BinaryOperatorNode
        GEN_COMP_GE,    // used by BinaryOperatorNode
        GEN_COMP_LT,    // used by BinaryOperatorNode
        GEN_COMP_LE,    // used by BinaryOperatorNode
        EXCEPT,         // used by BinaryOperatorNode

    }

    /// <summary>
    /// This delegate is used by XPath.Net internally. It isn't intended for use in application code.
    /// </summary>
    public delegate object BinaryOperator(IContextProvider provider, object arg1, object arg2);

    /// <summary>
    /// An AST node that represents a binary operator and its operands. 
    /// This class is used by XPath.Net internally. It isn't intended for use in application code.
    /// </summary>
    public class BinaryOperatorNode : AbstractNode
    {
        protected BinaryOperator _binaryOper;
        protected BinaryOperatorType _binaryOperatorType;
        private readonly XPath2ResultType _resultType;

        public BinaryOperatorNode(XPath2Context context, BinaryOperatorType operatorType, object node1, object node2,
            XPath2ResultType resultType)
            : base(context)
        {
            _binaryOperatorType = operatorType;
            _binaryOper = DeriveOperatorFromType(operatorType);
            _resultType = resultType;
            Add(node1);
            Add(node2);
        }

        private BinaryOperator DeriveOperatorFromType(BinaryOperatorType operatorType)
        {
            switch (operatorType)
            {
                case BinaryOperatorType.ADD:
                    return (provider, arg1, arg2) => ValueProxy.New(arg1) + ValueProxy.New(arg2);
                case BinaryOperatorType.SUBTRACT:
                    return (provider, arg1, arg2) => ValueProxy.New(arg1) - ValueProxy.New(arg2);
                case BinaryOperatorType.MULTIPLY:
                    return (provider, arg1, arg2) => ValueProxy.New(arg1) * ValueProxy.New(arg2);
                case BinaryOperatorType.DIVIDE:
                    return (provider, arg1, arg2) => ValueProxy.New(arg1) / ValueProxy.New(arg2);
                case BinaryOperatorType.INT_DIVIDE:
                    return (provider, arg1, arg2) => ValueProxy.op_IntegerDivide(ValueProxy.New(arg1), ValueProxy.New(arg2));
                case BinaryOperatorType.MODULO:
                    return (provider, arg1, arg2) => ValueProxy.New(arg1) % ValueProxy.New(arg2);
                case BinaryOperatorType.VAL_COMP_EQ:
                    return (provider, arg1, arg2) => CoreFuncs.OperatorEq(arg1, arg2);
                case BinaryOperatorType.VAL_COMP_NE:
                    return (provider, arg1, arg2) => CoreFuncs.Not(CoreFuncs.OperatorEq(arg1, arg2));
                case BinaryOperatorType.VAL_COMP_GT:
                    return (provider, arg1, arg2) => CoreFuncs.OperatorGt(arg1, arg2);
                case BinaryOperatorType.VAL_COMP_GE:
                    return (provider, arg1, arg2) => CoreFuncs.OperatorGt(arg1, arg2) == CoreFuncs.True ||
                                                     CoreFuncs.OperatorEq(arg1, arg2) == CoreFuncs.True ? CoreFuncs.True : CoreFuncs.False;
                case BinaryOperatorType.VAL_COMP_LT:
                    return (provider, arg1, arg2) => CoreFuncs.OperatorGt(arg2, arg1);
                case BinaryOperatorType.VAL_COMP_LE:
                    return (provider, arg1, arg2) => CoreFuncs.OperatorGt(arg2, arg1) == CoreFuncs.True ||
                                                     CoreFuncs.OperatorEq(arg1, arg2) == CoreFuncs.True ? CoreFuncs.True : CoreFuncs.False;
                case BinaryOperatorType.SAME_NODE:
                    return (provider, arg1, arg2) => CoreFuncs.SameNode(arg1, arg2);
                case BinaryOperatorType.PRECEDING_NODE:
                    return (provider, arg1, arg2) => CoreFuncs.PrecedingNode(arg1, arg2);
                case BinaryOperatorType.FOLLOWING_NODE:
                    return (provider, arg1, arg2) => CoreFuncs.FollowingNode(arg1, arg2);
                case BinaryOperatorType.UNION:
                    return (provider, arg1, arg2) => CoreFuncs.Union(Context, arg1, arg2);
                case BinaryOperatorType.UNION_OP:
                    return (provider, arg1, arg2) => CoreFuncs.Union(Context, arg1, arg2);
                case BinaryOperatorType.INTERSECT:
                    return (provider, arg1, arg2) => CoreFuncs.Intersect(Context, arg1, arg2);
                case BinaryOperatorType.GEN_COMP_EQ:
                    return (provider, arg1, arg2) => CoreFuncs.GeneralEQ(Context, arg1, arg2);
                case BinaryOperatorType.GEN_COMP_NE:
                    return (provider, arg1, arg2) => CoreFuncs.GeneralNE(Context, arg1, arg2);
                case BinaryOperatorType.GEN_COMP_GT:
                    return (provider, arg1, arg2) => CoreFuncs.GeneralGT(Context, arg1, arg2);
                case BinaryOperatorType.GEN_COMP_GE:
                    return (provider, arg1, arg2) => CoreFuncs.GeneralGE(Context, arg1, arg2);
                case BinaryOperatorType.GEN_COMP_LT:
                    return (provider, arg1, arg2) => CoreFuncs.GeneralLT(Context, arg1, arg2);
                case BinaryOperatorType.GEN_COMP_LE:
                    return (provider, arg1, arg2) => CoreFuncs.GeneralLE(Context, arg1, arg2);
                case BinaryOperatorType.EXCEPT:
                    return (provider, arg1, arg2) => CoreFuncs.Except(Context, arg1, arg2);
                default:
                    throw new NotImplementedException();
            }
        }

        public override object Execute(IContextProvider provider, object[] dataPool)
        {
            return _binaryOper(provider, this[0].Execute(provider, dataPool),
                this[1].Execute(provider, dataPool));
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
        public void SetOperatorType(BinaryOperatorType operatorType)
        {
            _binaryOperatorType = operatorType;
            _binaryOper = DeriveOperatorFromType(operatorType);
        }

        /// <inheritdoc/>
        public override string Render()
        {
            switch (_binaryOperatorType)
            {
                case BinaryOperatorType.ADD:
                    return this[0].Render() + " + " + this[1].Render();
                case BinaryOperatorType.SUBTRACT:
                    return this[0].Render() + " - " + this[1].Render();
                case BinaryOperatorType.MULTIPLY:
                    return this[0].Render() + " * " + this[1].Render();
                case BinaryOperatorType.DIVIDE:
                    return this[0].Render() + " div " + this[1].Render();
                case BinaryOperatorType.INT_DIVIDE:
                    return this[0].Render() + " idiv " + this[1].Render();
                case BinaryOperatorType.MODULO:
                    return this[0].Render() + " mod " + this[1].Render();
                case BinaryOperatorType.VAL_COMP_EQ:
                    return this[0].Render() + " eq " + this[1].Render();
                case BinaryOperatorType.VAL_COMP_NE:
                    return this[0].Render() + " ne " + this[1].Render();
                case BinaryOperatorType.VAL_COMP_GT:
                    return this[0].Render() + " gt " + this[1].Render();
                case BinaryOperatorType.VAL_COMP_GE:
                    return this[0].Render() + " ge " + this[1].Render();
                case BinaryOperatorType.VAL_COMP_LT:
                    return this[0].Render() + " lt " + this[1].Render();
                case BinaryOperatorType.VAL_COMP_LE:
                    return this[0].Render() + " le " + this[1].Render();
                case BinaryOperatorType.SAME_NODE:
                    return this[0].Render() + " is " + this[1].Render();
                case BinaryOperatorType.PRECEDING_NODE:
                    return this[0].Render() + " << " + this[1].Render();
                case BinaryOperatorType.FOLLOWING_NODE:
                    return this[0].Render() + " >> " + this[1].Render();
                case BinaryOperatorType.UNION:
                    return this[0].Render() + " union " + this[1].Render();
                case BinaryOperatorType.UNION_OP:
                    return this[0].Render() + " | " + this[1].Render();
                case BinaryOperatorType.INTERSECT:
                    return this[0].Render() + " intersect " + this[1].Render();
                case BinaryOperatorType.GEN_COMP_EQ:
                    return this[0].Render() + " = " + this[1].Render();
                case BinaryOperatorType.GEN_COMP_NE:
                    return this[0].Render() + " != " + this[1].Render();
                case BinaryOperatorType.GEN_COMP_GT:
                    return this[0].Render() + " > " + this[1].Render();
                case BinaryOperatorType.GEN_COMP_GE:
                    return this[0].Render() + " >= " + this[1].Render();
                case BinaryOperatorType.GEN_COMP_LT:
                    return this[0].Render() + " < " + this[1].Render();
                case BinaryOperatorType.GEN_COMP_LE:
                    return this[0].Render() + " <= " + this[1].Render();
                case BinaryOperatorType.EXCEPT:
                    return this[0].Render() + " except " + this[1].Render();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}