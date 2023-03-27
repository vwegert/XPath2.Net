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
        [Fact]
        public void AndExprNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("$a and $b");
            Assert.IsType<AndExprNode>(exp.ExpressionTree);
            var node = (AndExprNode)exp.ExpressionTree;
            Assert.Equal("$a and $b", node.Render());
            Assert.Equal("$a and $b", exp.Render());
        }
        [Fact]
        public void AndExprNode_WhenRenderingParsedExpression2_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("$a and $b and $c");
            Assert.IsType<AndExprNode>(exp.ExpressionTree);
            var node = (AndExprNode)exp.ExpressionTree;
            Assert.Equal("$a and $b and $c", node.Render());
            Assert.Equal("$a and $b and $c", exp.Render());
        }
        #endregion

        #region isolated tests for ArithmeticBinaryOperatorNode
        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingAddExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 + 2");
            Assert.IsType<ArithmeticBinaryOperatorNode>(exp.ExpressionTree);
            var node = (ArithmeticBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 + 2", node.Render());
            Assert.Equal("1 + 2", exp.Render());
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingSubtractExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 - 2");
            Assert.IsType<ArithmeticBinaryOperatorNode>(exp.ExpressionTree);
            var node = (ArithmeticBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 - 2", node.Render());
            Assert.Equal("1 - 2", exp.Render());
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingMultiplyExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 * 2");
            Assert.IsType<ArithmeticBinaryOperatorNode>(exp.ExpressionTree);
            var node = (ArithmeticBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 * 2", node.Render());
            Assert.Equal("1 * 2", exp.Render());
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingDivideExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 div 2");
            Assert.IsType<ArithmeticBinaryOperatorNode>(exp.ExpressionTree);
            var node = (ArithmeticBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 div 2", node.Render());
            Assert.Equal("1 div 2", exp.Render());
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingIntDivideExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 idiv 2");
            Assert.IsType<ArithmeticBinaryOperatorNode>(exp.ExpressionTree);
            var node = (ArithmeticBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 idiv 2", node.Render());
            Assert.Equal("1 idiv 2", exp.Render());
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingModuloExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 mod 2");
            Assert.IsType<ArithmeticBinaryOperatorNode>(exp.ExpressionTree);
            var node = (ArithmeticBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 mod 2", node.Render());
            Assert.Equal("1 mod 2", exp.Render());
        }
        #endregion

        #region isolated tests for AtomizedBinaryOperatorNode
        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingEQExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 eq 2");
            Assert.IsType<AtomizedBinaryOperatorNode>(exp.ExpressionTree);
            var node = (AtomizedBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 eq 2", node.Render());
            Assert.Equal("1 eq 2", exp.Render());
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingNEExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 ne 2");
            Assert.IsType<AtomizedBinaryOperatorNode>(exp.ExpressionTree);
            var node = (AtomizedBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 ne 2", node.Render());
            Assert.Equal("1 ne 2", exp.Render());
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingGTExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 gt 2");
            Assert.IsType<AtomizedBinaryOperatorNode>(exp.ExpressionTree);
            var node = (AtomizedBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 gt 2", node.Render());
            Assert.Equal("1 gt 2", exp.Render());
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingGEExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 ge 2");
            Assert.IsType<AtomizedBinaryOperatorNode>(exp.ExpressionTree);
            var node = (AtomizedBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 ge 2", node.Render());
            Assert.Equal("1 ge 2", exp.Render());
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingLTExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 lt 2");
            Assert.IsType<AtomizedBinaryOperatorNode>(exp.ExpressionTree);
            var node = (AtomizedBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 lt 2", node.Render());
            Assert.Equal("1 lt 2", exp.Render());
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingLEExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 le 2");
            Assert.IsType<AtomizedBinaryOperatorNode>(exp.ExpressionTree);
            var node = (AtomizedBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 le 2", node.Render());
            Assert.Equal("1 le 2", exp.Render());
        }
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
        [Fact]
        public void BinaryOperatorNode_WhenRenderingEQExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 = 2");
            Assert.IsType<BinaryOperatorNode>(exp.ExpressionTree);
            var node = (BinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 = 2", node.Render());
            Assert.Equal("1 = 2", exp.Render());
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingNEExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 != 2");
            Assert.IsType<BinaryOperatorNode>(exp.ExpressionTree);
            var node = (BinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 != 2", node.Render());
            Assert.Equal("1 != 2", exp.Render());
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingGTExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 > 2");
            Assert.IsType<BinaryOperatorNode>(exp.ExpressionTree);
            var node = (BinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 > 2", node.Render());
            Assert.Equal("1 > 2", exp.Render());
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingGEExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 >= 2");
            Assert.IsType<BinaryOperatorNode>(exp.ExpressionTree);
            var node = (BinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 >= 2", node.Render());
            Assert.Equal("1 >= 2", exp.Render());
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingLTExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 < 2");
            Assert.IsType<BinaryOperatorNode>(exp.ExpressionTree);
            var node = (BinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 < 2", node.Render());
            Assert.Equal("1 < 2", exp.Render());
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingLEExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 <= 2");
            Assert.IsType<BinaryOperatorNode>(exp.ExpressionTree);
            var node = (BinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 <= 2", node.Render());
            Assert.Equal("1 <= 2", exp.Render());
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingExceptExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 except 2");
            Assert.IsType<BinaryOperatorNode>(exp.ExpressionTree);
            var node = (BinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 except 2", node.Render());
            Assert.Equal("1 except 2", exp.Render());
        }
        #endregion

        #region isolated tests for ContextItemNode
        // TODO support ContextItemNode
        #endregion

        #region isolated tests for ExprNode
        [Fact]
        public void ExprNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1, 'a', $b");
            Assert.IsType<ExprNode>(exp.ExpressionTree);
            var node = (ExprNode)exp.ExpressionTree;
            Assert.Equal("1, 'a', $b", node.Render());
            Assert.Equal("1, 'a', $b", exp.Render());
        }
        #endregion

        #region isolated tests for FilterExprNode
        // TODO support FilterExprNode
        #endregion

        #region isolated tests for ForNode
        [Fact]
        public void ForNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("for $foo1 in $bar1 return 1");
            Assert.IsType<ForNode>(exp.ExpressionTree);
            var node = (ForNode)exp.ExpressionTree;
            Assert.Equal("for $foo1 in $bar1 return 1", node.Render());
            Assert.Equal("for $foo1 in $bar1 return 1", exp.Render());
        }

        [Fact]
        public void ForNode_WhenRenderingParsedExpression2_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("for $foo1 in $bar1, $foo2 in $bar2 return 1");
            Assert.IsType<ForNode>(exp.ExpressionTree);
            var node = (ForNode)exp.ExpressionTree;
            Assert.Equal("for $foo1 in $bar1, $foo2 in $bar2 return 1", node.Render());
            Assert.Equal("for $foo1 in $bar1, $foo2 in $bar2 return 1", exp.Render());
        }

        [Fact]
        public void ForNode_WhenRenderingParsedExpression3_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("for $foo1 in $bar1, $foo2 in $bar2, $foo3 in $bar3 return 1");
            Assert.IsType<ForNode>(exp.ExpressionTree);
            var node = (ForNode)exp.ExpressionTree;
            Assert.Equal("for $foo1 in $bar1, $foo2 in $bar2, $foo3 in $bar3 return 1", node.Render());
            Assert.Equal("for $foo1 in $bar1, $foo2 in $bar2, $foo3 in $bar3 return 1", exp.Render());
        }

        [Fact]
        public void ForNode_WhenRenderingParsedExpression4_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("for $foo1 in $bar1, $foo2 in $bar2, $foo3 in $bar3, $foo4 in $bar4 return 1");
            Assert.IsType<ForNode>(exp.ExpressionTree);
            var node = (ForNode)exp.ExpressionTree;
            Assert.Equal("for $foo1 in $bar1, $foo2 in $bar2, $foo3 in $bar3, $foo4 in $bar4 return 1", node.Render());
            Assert.Equal("for $foo1 in $bar1, $foo2 in $bar2, $foo3 in $bar3, $foo4 in $bar4 return 1", exp.Render());
        }
        #endregion

        #region isolated tests for FuncNode
        [Fact]
        public void FuncNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("current-date()");
            Assert.IsType<FuncNode>(exp.ExpressionTree);
            var node = (FuncNode)exp.ExpressionTree;
            // rendering a FuncNode will introduce the namespace prefix
            Assert.Equal("fn:current-date()", node.Render());
            Assert.Equal("fn:current-date()", exp.Render());
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression2_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("fn:current-date()");
            Assert.IsType<FuncNode>(exp.ExpressionTree);
            var node = (FuncNode)exp.ExpressionTree;
            Assert.Equal("fn:current-date()", node.Render());
            Assert.Equal("fn:current-date()", exp.Render());
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression3_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("ceiling(1)");
            Assert.IsType<FuncNode>(exp.ExpressionTree);
            var node = (FuncNode)exp.ExpressionTree;
            // rendering a FuncNode will introduce the namespace prefix
            Assert.Equal("fn:ceiling(1)", node.Render());
            Assert.Equal("fn:ceiling(1)", exp.Render());
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression4_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("fn:ceiling(1)");
            Assert.IsType<FuncNode>(exp.ExpressionTree);
            var node = (FuncNode)exp.ExpressionTree;
            Assert.Equal("fn:ceiling(1)", node.Render());
            Assert.Equal("fn:ceiling(1)", exp.Render());
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression5_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("concat(1, 'a', 2, 'b')");
            Assert.IsType<FuncNode>(exp.ExpressionTree);
            var node = (FuncNode)exp.ExpressionTree;
            // rendering a FuncNode will introduce the namespace prefix
            Assert.Equal("fn:concat(1, 'a', 2, 'b')", node.Render());
            Assert.Equal("fn:concat(1, 'a', 2, 'b')", exp.Render());
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression6_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("fn:concat(1, 'a', 2, 'b')");
            Assert.IsType<FuncNode>(exp.ExpressionTree);
            var node = (FuncNode)exp.ExpressionTree;
            Assert.Equal("fn:concat(1, 'a', 2, 'b')", node.Render());
            Assert.Equal("fn:concat(1, 'a', 2, 'b')", exp.Render());
        }
        #endregion

        #region isolated tests for IfNode
        [Fact]
        public void IfNode_WhenRenderingParsedExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("if ( 1 < 2 ) then 3 else 4");
            Assert.IsType<IfNode>(exp.ExpressionTree);
            var node = (IfNode)exp.ExpressionTree;
            Assert.Equal("if ( 1 < 2 ) then 3 else 4", node.Render());
            Assert.Equal("if ( 1 < 2 ) then 3 else 4", exp.Render());
        }
        #endregion

        #region isolated tests for OrderedBinaryOperatorNode
        [Fact]
        public void OrderedBinaryOperatorNode_WhenRenderingUnionExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 union 2");
            Assert.IsType<OrderedBinaryOperatorNode>(exp.ExpressionTree);
            var node = (OrderedBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 union 2", node.Render());
            Assert.Equal("1 union 2", exp.Render());
        }

        [Fact]
        public void OrderedBinaryOperatorNode_WhenRenderingIntersectExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 intersect 2");
            Assert.IsType<OrderedBinaryOperatorNode>(exp.ExpressionTree);
            var node = (OrderedBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 intersect 2", node.Render());
            Assert.Equal("1 intersect 2", exp.Render());
        }
        #endregion

        #region isolated tests for OrExprNode
        [Fact]
        public void OrExprNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("$a or $b");
            Assert.IsType<OrExprNode>(exp.ExpressionTree);
            var node = (OrExprNode)exp.ExpressionTree;
            Assert.Equal("$a or $b", node.Render());
            Assert.Equal("$a or $b", exp.Render());
        }
        [Fact]
        public void OrExprNode_WhenRenderingParsedExpression2_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("$a or $b or $c");
            Assert.IsType<OrExprNode>(exp.ExpressionTree);
            var node = (OrExprNode)exp.ExpressionTree;
            Assert.Equal("$a or $b or $c", node.Render());
            Assert.Equal("$a or $b or $c", exp.Render());
        }
        #endregion

        #region isolated tests for PathExprNode
        // TODO support PathExprNode
        #endregion

        #region isolated tests for PathStep
        // TODO support PathStep
        #endregion

        #region isolated tests for RangeNode
        [Fact]
        public void RangeNode_WhenRenderingParsedExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 to 4");
            Assert.IsType<RangeNode>(exp.ExpressionTree);
            var node = (RangeNode)exp.ExpressionTree;
            Assert.Equal("1 to 4", node.Render());
            Assert.Equal("1 to 4", exp.Render());
        }

        [Fact]
        public void RangeNode_WhenRenderingChangedExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 to 4");
            Assert.IsType<RangeNode>(exp.ExpressionTree);
            var node = (RangeNode)exp.ExpressionTree;
            ((ValueNode)node[0]).SetValue(17);
            ((ValueNode)node[1]).SetValue(23);
            Assert.Equal("17 to 23", node.Render());
            Assert.Equal("17 to 23", exp.Render());
        }
        #endregion

        #region isolated tests for SingletonBinaryOperatorNode
        [Fact]
        public void SingletonBinaryOperatorNode_WhenRenderingSameExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 is 2");
            Assert.IsType<SingletonBinaryOperatorNode>(exp.ExpressionTree);
            var node = (SingletonBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 is 2", node.Render());
            Assert.Equal("1 is 2", exp.Render());
        }

        [Fact]
        public void SingletonBinaryOperatorNode_WhenRenderingPrecedingExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 << 2");
            Assert.IsType<SingletonBinaryOperatorNode>(exp.ExpressionTree);
            var node = (SingletonBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 << 2", node.Render());
            Assert.Equal("1 << 2", exp.Render());
        }

        [Fact]
        public void SingletonBinaryOperatorNode_WhenRenderingFollowingExpression_ShouldReturnCorrectExpression()
        {
            XPath2Expression exp = XPath2Expression.Compile("1 >> 2");
            Assert.IsType<SingletonBinaryOperatorNode>(exp.ExpressionTree);
            var node = (SingletonBinaryOperatorNode)exp.ExpressionTree;
            Assert.Equal("1 >> 2", node.Render());
            Assert.Equal("1 >> 2", exp.Render());
        }
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
