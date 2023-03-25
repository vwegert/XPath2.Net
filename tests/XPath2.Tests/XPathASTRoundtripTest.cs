using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmhelp.XPath2;
using Wmhelp.XPath2.AST;
using Xunit;

namespace XPath2.Tests
{
    [Collection("Sequential")]
    public class XPathASTRoundtripTest
    {

        #region isolated tests for AndExprNode
        // TODO support AndExprNode
        #endregion

        #region isolated tests for ArithmeticBinaryOperatorNode
        // TODO support ArithmeticBinaryOperatorNode
        #endregion

        #region isolated tests for AtomizedBinaryOperatorNode
        // TODO support AtomizedBinaryOperatorNode
        #endregion

        #region isolated tests for AtomizedUnaryOperatorNode
        // TODO support AtomizedUnaryOperatorNode
        #endregion

        #region isolated tests for BinaryOperatorNode
        // TODO support BinaryOperatorNode
        #endregion

        #region isolated tests for ContextItemNode
        // TODO support ContextItemNode
        #endregion

        #region isolated tests for ExprNode
        // TODO support ExprNode
        #endregion

        #region isolated tests for FilterExprNode
        // TODO support FilterExprNode
        #endregion

        #region isolated tests for ForNode
        // TODO support ForNode
        #endregion

        #region isolated tests for FuncNode
        // TODO support FuncNode
        #endregion

        #region isolated tests for IfNode
        // TODO support IfNode
        #endregion

        #region isolated tests for OrderedBinaryOperatorNode
        // TODO support OrderedBinaryOperatorNode
        #endregion

        #region isolated tests for OrExprNode
        // TODO support OrExprNode
        #endregion

        #region isolated tests for PathExprNode
        // TODO support PathExprNode
        #endregion

        #region isolated tests for PathStep
        // TODO support PathStep
        #endregion

        #region isolated tests for RangeNode
        // TODO support RangeNode
        #endregion

        #region isolated tests for SingletonBinaryOperatorNode
        // TODO support SingletonBinaryOperatorNode
        #endregion

        #region isolated tests for UnaryOperatorNode
        // TODO support UnaryOperatorNode
        #endregion

        #region isolated tests for ValueNode
        [Fact]
        public void ValueNode_AfterChangingValueToInteger_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("123");
            Assert.IsType<ValueNode>(exp.ExpressionTree);
            var node = (ValueNode)exp.ExpressionTree;
            node.SetValue(42);
            Assert.Equal("42", node.Render());
            Assert.Equal("42", exp.Render());
        }

        [Fact]
        public void ValueNode_AfterChangingValueToString_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("'abc'");
            Assert.IsType<ValueNode>(exp.ExpressionTree);
            var node = (ValueNode)exp.ExpressionTree;
            node.SetValue("FooBar");
            Assert.Equal("'FooBar'", node.Render());
            Assert.Equal("'FooBar'", exp.Render());
        }
        #endregion

        #region isolated tests for VarRefNode
        [Fact]
        public void VarRefNode_AfterChangingLocalName_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("$var");
            Assert.IsType<VarRefNode>(exp.ExpressionTree);
            var node = (VarRefNode)exp.ExpressionTree;
            node.SetVarName("foo");
            Assert.Equal("$foo", node.Render());
            Assert.Equal("$foo", exp.Render());
        }

        [Fact]
        public void VarRefNode_AfterChangingQName_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("$var");
            Assert.IsType<VarRefNode>(exp.ExpressionTree);
            var node = (VarRefNode)exp.ExpressionTree;
            node.SetVarName("foo", "xyz");
            Assert.Equal("$xyz:foo", node.Render());
            Assert.Equal("$xyz:foo", exp.Render());
        }
        #endregion

    }
}
