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

        #region auxiliary method to keep actual tests less repetitive
        delegate void NodeModifier<T>(T node) where T : AbstractNode;

        private void PerformASTRoundtripTest<T>(string originalExpression, NodeModifier<T>? modifier = null, string? targetExpression = null) where T : AbstractNode
        {
            // compile the expression
            XPath2Expression exp = XPath2Expression.Compile(originalExpression);

            // check the root node type and cast
            Assert.IsType<T>(exp.ExpressionTree);
            var node = (T)exp.ExpressionTree;

            // if desired, perform a modification on the AST
            if (modifier != null)
            {
                modifier(node);
            }

            // check that the result matches the expectations
            var expected = targetExpression ?? originalExpression;
            Assert.Equal(expected, node.Render());
            Assert.Equal(expected, exp.Render());
        }
        #endregion


        #region isolated tests for AndExprNode
        [Fact]
        public void AndExprNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AndExprNode>("$a and $b");
        }
        [Fact]
        public void AndExprNode_WhenRenderingParsedExpression2_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AndExprNode>("$a and $b and $c");
        }
        #endregion

        #region isolated tests for ArithmeticBinaryOperatorNode
        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingAddExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ArithmeticBinaryOperatorNode>("1 + 2");
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingSubtractExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ArithmeticBinaryOperatorNode>("1 - 2");
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingMultiplyExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ArithmeticBinaryOperatorNode>("1 * 2");
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingDivideExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ArithmeticBinaryOperatorNode>("1 div 2");
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingIntDivideExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ArithmeticBinaryOperatorNode>("1 idiv 2");
        }

        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenRenderingModuloExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ArithmeticBinaryOperatorNode>("1 mod 2");
        }
        [Fact]
        public void ArithmeticBinaryOperatorNode_WhenChangingOperator_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ArithmeticBinaryOperatorNode>(
                "1 div 2",
                (node) => { node.SetOperatorType(BinaryOperatorType.INT_DIVIDE); },
                "1 idiv 2");
        }
        #endregion

        #region isolated tests for AtomizedBinaryOperatorNode
        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingEQExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedBinaryOperatorNode>("1 eq 2");
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingNEExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedBinaryOperatorNode>("1 ne 2");
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingGTExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedBinaryOperatorNode>("1 gt 2");
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingGEExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedBinaryOperatorNode>("1 ge 2");
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingLTExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedBinaryOperatorNode>("1 lt 2");
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenRenderingLEExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedBinaryOperatorNode>("1 le 2");
        }

        [Fact]
        public void AtomizedBinaryOperatorNode_WhenChanginOperator_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedBinaryOperatorNode>(
                "1 le 2",
                (node) => { node.SetOperatorType(BinaryOperatorType.VAL_COMP_GE); },
                "1 ge 2");
        }
        #endregion

        #region isolated tests for AtomizedUnaryOperatorNode
        [Fact]
        public void AtomizedUnaryOperatorNode_AfterChangingToPlus_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedUnaryOperatorNode>(
                "-42",
                (node) => { node.SetOperatorType(UnaryOperatorType.PLUS); },
                "+42");
        }

        [Fact]
        public void AtomizedUnaryOperatorNode_AfterChangingToMinus_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<AtomizedUnaryOperatorNode>(
                "+42",
                (node) => { node.SetOperatorType(UnaryOperatorType.MINUS); },
                "-42");
        }
        #endregion

        #region isolated tests for BinaryOperatorNode
        [Fact]
        public void BinaryOperatorNode_WhenRenderingEQExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<BinaryOperatorNode>("1 = 2"); ;
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingNEExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<BinaryOperatorNode>("1 != 2");
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingGTExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<BinaryOperatorNode>("1 > 2");
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingGEExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<BinaryOperatorNode>("1 >= 2");
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingLTExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<BinaryOperatorNode>("1 < 2");
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingLEExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<BinaryOperatorNode>("1 <= 2");
        }

        [Fact]
        public void BinaryOperatorNode_WhenRenderingExceptExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<BinaryOperatorNode>("1 except 2");
        }

        [Fact]
        public void BinaryOperatorNode_WhenChangingOperator_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<BinaryOperatorNode>(
                "1 < 2",
                (node) => { node.SetOperatorType(BinaryOperatorType.GEN_COMP_GT); },
                "1 > 2");
        }
        #endregion

        #region isolated tests for ContextItemNode
        // TODO support ContextItemNode
        #endregion

        #region isolated tests for ExprNode
        [Fact]
        public void ExprNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ExprNode>("(1, 'a', $b)");
        }
        #endregion

        #region isolated tests for FilterExprNode
        // TODO support FilterExprNode
        #endregion

        #region isolated tests for ForNode
        [Fact]
        public void ForNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ForNode>("for $foo1 in $bar1 return 1");
        }

        [Fact]
        public void ForNode_WhenRenderingParsedExpression2_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ForNode>("for $foo1 in $bar1, $foo2 in $bar2 return 1");
        }

        [Fact]
        public void ForNode_WhenRenderingParsedExpression3_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ForNode>("for $foo1 in $bar1, $foo2 in $bar2, $foo3 in $bar3 return 1");
        }

        [Fact]
        public void ForNode_WhenRenderingParsedExpression4_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ForNode>("for $foo1 in $bar1, $foo2 in $bar2, $foo3 in $bar3, $foo4 in $bar4 return 1");
        }

        [Fact]
        public void ForNode_WhenChangingReferenceName_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ForNode>(
                "for $foo1 in $bar1 return 1",
                (node) => { ((VarRefNode)node[0]).SetVarName("baz42"); },
                "for $foo1 in $baz42 return 1");
        }

        [Fact]
        public void ForNode_WhenChangingVariableName_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ForNode>(
                "for $foo1 in $bar1 return 1",
                (node) => { node.SetVarName("foo42"); },
                "for $foo42 in $bar1 return 1");
        }
        [Fact]
        public void ForNode_WhenChangingVariableNameWithPrefix_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ForNode>(
                "for $foo1 in $bar1 return 1",
                (node) => { node.SetVarName("foo42", "pfx"); },
                "for $pfx:foo42 in $bar1 return 1");
        }
        #endregion

        #region isolated tests for FuncNode
        [Fact]
        public void FuncNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            // rendering a FuncNode will introduce the namespace prefix
            PerformASTRoundtripTest<FuncNode>("current-date()", null, "fn:current-date()");
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression2_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<FuncNode>("fn:current-date()");
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression3_ShouldReturnCorrectExpression()
        {
            // rendering a FuncNode will introduce the namespace prefix
            PerformASTRoundtripTest<FuncNode>("ceiling(1)", null, "fn:ceiling(1)");
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression4_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<FuncNode>("fn:ceiling(1)");
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression5_ShouldReturnCorrectExpression()
        {
            // rendering a FuncNode will introduce the namespace prefix
            PerformASTRoundtripTest<FuncNode>("concat(1, 'a', 2, 'b')", null, "fn:concat(1, 'a', 2, 'b')");
        }

        [Fact]
        public void FuncNode_WhenRenderingParsedExpression6_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<FuncNode>("fn:concat(1, 'a', 2, 'b')");
        }
        #endregion

        #region isolated tests for IfNode
        [Fact]
        public void IfNode_WhenRenderingParsedExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<IfNode>("if ( 1 < 2 ) then 3 else 4");
        }
        #endregion

        #region isolated tests for OrderedBinaryOperatorNode
        [Fact]
        public void OrderedBinaryOperatorNode_WhenRenderingUnionExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<OrderedBinaryOperatorNode>("1 union 2");
        }

        [Fact]
        public void OrderedBinaryOperatorNode_WhenRenderingIntersectExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<OrderedBinaryOperatorNode>("1 intersect 2");
        }
        #endregion

        #region isolated tests for OrExprNode
        [Fact]
        public void OrExprNode_WhenRenderingParsedExpression1_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<OrExprNode>("$a or $b");
        }
        [Fact]
        public void OrExprNode_WhenRenderingParsedExpression2_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<OrExprNode>("$a or $b or $c");
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
            PerformASTRoundtripTest<RangeNode>("1 to 4");
        }

        [Fact]
        public void RangeNode_WhenRenderingChangedExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<RangeNode>(
                "1 to 4",
                (node) =>
                {
                    ((ValueNode)node[0]).SetValue(17);
                    ((ValueNode)node[1]).SetValue(23);
                },
                "17 to 23");
        }
        #endregion

        #region isolated tests for SingletonBinaryOperatorNode
        [Fact]
        public void SingletonBinaryOperatorNode_WhenRenderingSameExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<SingletonBinaryOperatorNode>("1 is 2");
        }

        [Fact]
        public void SingletonBinaryOperatorNode_WhenRenderingPrecedingExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<SingletonBinaryOperatorNode>("1 << 2");
        }

        [Fact]
        public void SingletonBinaryOperatorNode_WhenRenderingFollowingExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<SingletonBinaryOperatorNode>("1 >> 2");
        }
        #endregion

        #region isolated tests for UnaryOperatorNode
        [Fact]
        public void UnaryOperatorNode_WhenRenderingParsedSomeExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>("some $a in (1, 2, 3) satisfies $a = 1");
        }

        [Fact]
        public void UnaryOperatorNode_WhenRenderingParsedEveryExpression_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>("every $a in (1, 2, 3) satisfies $a = 1");
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingSomeToEvery_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>(
                "some $a in (1, 2, 3) satisfies $a = 1",
                (node) => { node.SetOperatorType(UnaryOperatorType.EVERY); },
                "every $a in (1, 2, 3) satisfies $a = 1");
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingEveryToSome_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>(
                "every $a in (1, 2, 3) satisfies $a = 1",
                (node) => { node.SetOperatorType(UnaryOperatorType.SOME); },
                "some $a in (1, 2, 3) satisfies $a = 1");
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingInstanceOfToTreatAs_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>(
                "5 instance of xs:integer",
                (node) => { node.SetOperatorType(UnaryOperatorType.TREAT_AS); },
                "5 treat as xs:integer");
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingTreatAsToInstanceOf_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>(
                "5 treat as xs:integer",
                (node) => { node.SetOperatorType(UnaryOperatorType.INSTANCE_OF); },
                "5 instance of xs:integer");
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingInstanceOfToCastable_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>(
                "5 instance of xs:integer",
                (node) => { node.SetOperatorType(UnaryOperatorType.CASTABLE); },
                "5 castable as xs:integer");
        }

        [Fact]
        public void UnaryOperatorNode_AfterChangingInstanceOfToCastAs_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>(
                "5 instance of xs:integer",
                (node) => { node.SetOperatorType(UnaryOperatorType.CAST_TO); },
                "5 cast as xs:integer");
        }

        [Fact]
        public void UnaryOperatorNode_UnchangedRootPathExpr_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<UnaryOperatorNode>("/");
        }

        // TODO Test for CAST_TO_ITEM (requires FunctionNode rendering)
        #endregion

        #region isolated tests for ValueNode
        [Fact]
        public void ValueNode_AfterChangingValueToInteger_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ValueNode>(
                "123",
                (node) => { node.SetValue(42); },
                "42");
        }

        [Fact]
        public void ValueNode_AfterChangingValueToString_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<ValueNode>(
                "'abc'",
                (node) => { node.SetValue("FooBar"); },
                "'FooBar'");
        }
        #endregion

        #region isolated tests for VarRefNode
        [Fact]
        public void VarRefNode_AfterChangingLocalName_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<VarRefNode>(
                "$var",
                (node) => { node.SetVarName("foo"); },
                "$foo");
        }

        [Fact]
        public void VarRefNode_AfterChangingQName_ShouldReturnCorrectExpression()
        {
            PerformASTRoundtripTest<VarRefNode>(
                "$var",
                (node) => { node.SetVarName("foo", "xyz"); },
                "$xyz:foo");
        }
        #endregion

    }
}
