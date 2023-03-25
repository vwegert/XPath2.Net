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
        [Fact]
        public void AtomizedUnaryOperatorNode_AfterChangingToPlus_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("- 42");
            Assert.IsType<AtomizedUnaryOperatorNode>(exp.ExpressionTree);
            var node = (AtomizedUnaryOperatorNode)exp.ExpressionTree;
            node.SetOperatorType(UnaryOperatorType.PLUS);
            Assert.Equal("+42", node.Render());
            Assert.Equal("+42", exp.Render());
        }

        [Fact]
        public void AtomizedUnaryOperatorNode_AfterChangingToMinus_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("+ 42");
            Assert.IsType<AtomizedUnaryOperatorNode>(exp.ExpressionTree);
            var node = (AtomizedUnaryOperatorNode)exp.ExpressionTree;
            node.SetOperatorType(UnaryOperatorType.MINUS);
            Assert.Equal("-42", node.Render());
            Assert.Equal("-42", exp.Render());
        }
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
        [Fact]
        public void UnaryOperatorNode_AfterChangingSomeToEvery_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("some $a in (1, 2, 3) satisfies $a = 1");
            Assert.IsType<UnaryOperatorNode>(exp.ExpressionTree);
            var node = (UnaryOperatorNode)exp.ExpressionTree;
            node.SetOperatorType(UnaryOperatorType.EVERY);
            Assert.StartsWith("every", node.Render());
            Assert.StartsWith("every", exp.Render());
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingEveryToSome_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("every $a in (1, 2, 3) satisfies $a = 1");
            Assert.IsType<UnaryOperatorNode>(exp.ExpressionTree);
            var node = (UnaryOperatorNode)exp.ExpressionTree;
            node.SetOperatorType(UnaryOperatorType.SOME);
            Assert.StartsWith("some", node.Render());
            Assert.StartsWith("some", exp.Render());
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingInstanceOfToTreatAs_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("5 instance of xs:integer");
            Assert.IsType<UnaryOperatorNode>(exp.ExpressionTree);
            var node = (UnaryOperatorNode)exp.ExpressionTree;
            node.SetOperatorType(UnaryOperatorType.TREAT_AS);
            Assert.StartsWith("5 treat as", node.Render());
            Assert.StartsWith("5 treat as", exp.Render());
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingTreatAsToInstanceOf_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("5 treat as xs:integer");
            Assert.IsType<UnaryOperatorNode>(exp.ExpressionTree);
            var node = (UnaryOperatorNode)exp.ExpressionTree;
            node.SetOperatorType(UnaryOperatorType.INSTANCE_OF);
            Assert.StartsWith("5 instance of", node.Render());
            Assert.StartsWith("5 instance of", exp.Render());
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingInstanceOfToCastable_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("5 instance of xs:integer");
            Assert.IsType<UnaryOperatorNode>(exp.ExpressionTree);
            var node = (UnaryOperatorNode)exp.ExpressionTree;
            node.SetOperatorType(UnaryOperatorType.CASTABLE);
            Assert.StartsWith("5 castable as", node.Render());
            Assert.StartsWith("5 castable as", exp.Render());
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingInstanceOfToCastAs_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("5 instance of xs:integer");
            Assert.IsType<UnaryOperatorNode>(exp.ExpressionTree);
            var node = (UnaryOperatorNode)exp.ExpressionTree;
            node.SetOperatorType(UnaryOperatorType.CAST_TO);
            Assert.StartsWith("5 cast as", node.Render());
            Assert.StartsWith("5 cast as", exp.Render());
        }

        [Fact]
        public void UnaryOperatorNode_UnchangedRootPathExpr_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("/");
            Assert.IsType<UnaryOperatorNode>(exp.ExpressionTree);
            var node = (UnaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("/", node.Render());
            Assert.Equal("/", exp.Render());
        }

        // TODO Test for CAST_TO_ITEM (requires FunctionNode rendering)
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
