using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Wmhelp.XPath2.Iterator.ChildOverDescendantsNodeIterator;
using System.Xml.Linq;
using System.Xml.Schema;
using Wmhelp.XPath2.MS;

namespace Wmhelp.XPath2.AST
{
    /// <summary>
    /// Auxiliary class to render a textual representation of an abstract syntax tree (AST).
    /// This class is intended to be used by developers to understand how the parser works and
    /// what the exact layout and contents of the AST nodes are under which circumstances.
    /// It is probably less useful for actual production code.
    /// </summary>
    public class ASTToTextConverter
    {
        public String RenderExpressionSyntaxTreeToText(XPath2Expression expression)
        {
            StringWriter target = new StringWriter();
            RenderExpressionSyntaxTreeToText(expression, target);
            return target.ToString();
        }

        public void RenderExpressionSyntaxTreeToText(XPath2Expression expression, TextWriter target)
        {
            RenderObject(target, 0, "expression", expression.GetType().ToString(), new Dictionary<string, string>()
            {
                { "Expression", expression.Expression }
            });
            RenderNode(target, 1, "ExpressionTree", (dynamic)(expression.ExpressionTree));
        }

        #region node-type specific rendering

        private void RenderNode(TextWriter target, int indent, string name, AbstractNode node)
        {
            RenderObject(target, indent, name, node.GetType().ToString(), GetNodeAttributes((dynamic)node));
            for (int i = 0; i < node.Count; i++)
            {
                RenderNode(target, indent + 1, $"child[{i}]", (dynamic)node[i]);
            }
        }

        private void RenderNode(TextWriter target, int indent, string name, PathExprNode node)
        {
            RenderObject(target, indent, name, node.GetType().ToString(), GetNodeAttributes((dynamic)node));
            for (int i = 0; i < node.Count; i++)
            {
                RenderNode(target, indent + 1, $"child[{i}]", (dynamic)node[i]);
            }
            for (int i = 0; i < node.Steps.Length; i++)
            {
                RenderStep(target, indent + 1, $"step[{i}]", (dynamic)node.Steps[i]);
            }
        }

        private void RenderStep(TextWriter target, int indent, string name, PathStep step)
        {
            RenderObject(target, indent, name, step.GetType().ToString(), new Dictionary<string, string>()
            {
                { "Type", step.ExpressionType.ToString() },
            });
            if (step.Node != null)
            {
                RenderNode(target, indent + 1, $"Node", (dynamic)step.Node);
            }
            if (step.NodeTest != null)
            {
                RenderStepTest(target, indent + 1, $"NodeTest", (dynamic)step.NodeTest);
            }
        }

        private void RenderStepTest(TextWriter target, int indent, string name, object test)
        {
            RenderObject(target, indent, name, test.GetType().ToString(), new Dictionary<string, string>()
            {
            });

        }

        private void RenderStepTest(TextWriter target, int indent, string name, SequenceType test)
        {
            RenderObject(target, indent, name, test.GetType().ToString(), new Dictionary<string, string>()
            {
                { "TypeCode", test.TypeCode.ToString() },
                { "NameTest", test.NameTest == null ? "(null)" : test.NameTest.ToString() },
                { "Cardinality", test.Cardinality.ToString() },
                { "SchemaType", test.SchemaType == null ? "(null)" : test.SchemaType.ToString() },
                { "SchemaElement", test.SchemaElement == null ? "(null)" : test.SchemaElement.ToString() },
                { "SchemaAttribute", test.SchemaAttribute == null ? "(null)" : test.SchemaAttribute.ToString() },
                { "Nillable", test.Nillable.ToString() },
                { "ParameterType", test.ParameterType == null ? "(null)" : test.ParameterType.ToString() },
                { "ItemType", test.ItemType == null ? "(null)" : test.ItemType.ToString() },
                { "IsNode", test.IsNode.ToString() }
            });
        }

        private void RenderStepTest(TextWriter target, int indent, string name, XmlQualifiedNameTest test)
        {
            RenderObject(target, indent, name, test.GetType().ToString(), new Dictionary<string, string>()
            {
                { "IsEmpty", test.IsEmpty.ToString() },
                { "Name", test.Name == null ? "(null)" : test.Name.ToString() },
                { "Namespace", test.Namespace == null ? "(null)" : test.Namespace.ToString() },
                { "Cardinality", test.IsExclude.ToString() }
            });
        }


        private IDictionary<string, string>? GetNodeAttributes(AbstractNode node)
        {
            return null;
        }

        private IDictionary<string, string>? GetNodeAttributes(ForNode node)
        {
            return new Dictionary<string, string>()
            {
                { "VarName", node.VarName.ToString() }
            };
        }

        private IDictionary<string, string>? GetNodeAttributes(FuncNode node)
        {
            return new Dictionary<string, string>()
            {
                { "Name", node.Name.ToString() },
                { "Namespace", node.Namespace.ToString() },
            };
        }

        private IDictionary<string, string>? GetNodeAttributes(ValueNode node)
        {
            return new Dictionary<string, string>()
            {
                { "Value", node.Content.ToString() }
            };
        }

        private IDictionary<string, string>? GetNodeAttributes(VarRefNode node)
        {
            return new Dictionary<string, string>()
            {
                { "VarName", node.VarName.ToString() }
            };
        }

        private IDictionary<string, string>? GetNodeAttributes(UnaryOperatorNode node)
        {
            return new Dictionary<string, string>()
            {
                { "OperatorType", node.OperatorType.ToString() },
                { "IsString", node.IsString.ToString() },
                { "ResultType", node.ResultType.ToString() }
            };
        }

        private IDictionary<string, string>? GetNodeAttributes(BinaryOperatorNode node)
        {
            return new Dictionary<string, string>()
            {
                { "OperatorType", node.OperatorType.ToString() },
                { "ResultType", node.ResultType.ToString() }
            };
        }

        private IDictionary<string, string>? GetNodeAttributes(PathExprNode node)
        {
            return new Dictionary<string, string>()
            {
                { "Unordered", node.Unordered.ToString() }
            };
        }

        #endregion

        #region low-level rendering

        private void RenderObject(TextWriter target, int indent, string name, string type, IDictionary<string, string>? attributes = null)
        {
            const int INDENT_WIDTH = 5;
            var prefix = new String(' ', indent * INDENT_WIDTH);
            var width = 2 + name.Length + 2 + type.Length + 2;
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    var attributeWidth = 2 + attribute.Key.Length + 2 + attribute.Value.Length + 2;
                    width = attributeWidth > width ? attributeWidth : width;
                }
            }
            var horizontalLine = "+" + new String('-', width - 2) + "+";

            target.WriteLine(prefix + horizontalLine);
            var headerLineContents = name + ": " + type;
            target.WriteLine(prefix + "| " + headerLineContents + new String(' ', width - (4 + headerLineContents.Length)) + " |");
            target.WriteLine(prefix + horizontalLine);

            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    var attributeLineContents = attribute.Key + ": " + attribute.Value;
                    target.WriteLine(prefix + "| " + attributeLineContents + new String(' ', width - (4 + attributeLineContents.Length)) + " |");
                }
                target.WriteLine(prefix + horizontalLine);
            }

            target.WriteLine();
        }

        #endregion
    }
}
