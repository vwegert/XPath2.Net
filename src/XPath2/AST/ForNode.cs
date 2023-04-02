// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.

using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace Wmhelp.XPath2.AST
{
    /// <summary>
    /// This class is used by XPath.Net internally. It isn't intended for use in application code.
    /// </summary>
    public sealed class ForNode : AbstractNode
    {
        private Tokenizer.VarName _varName;
        private NameBinder.ReferenceLink _varRef;

        public Tokenizer.VarName VarName => _varName;

        public ForNode(XPath2Context context, Tokenizer.VarName varName, object expr)
            : base(context)
        {
            _varName = varName;
            Add(expr);
        }

        public void AddTail(object expr)
        {
            if (Count == 1)
                Add(expr);
            else
                ((ForNode)this[1]).AddTail(expr);
        }

        public override void Bind()
        {
            this[0].Bind();
            XmlQualifiedName qname = QNameParser.Parse(_varName.ToString(),
                Context.NamespaceManager, Context.NameTable);
            _varRef = Context.RunningContext.NameBinder.PushVar(qname);
            this[1].Bind();
            Context.RunningContext.NameBinder.PopVar();
        }

        /// <summary>
        /// Changes the loop variable name of this AST node. This method will attempt to fix the bindings if told to do so.
        /// Caution: Using this method might render the AST unusable for execution and evaluation, e. g. by breaking the variable references.
        /// </summary>
        /// <param name="localName"></param>
        /// <param name="prefix"></param>
        /// <param name="recreateBindings">whether to allow recreating of the variable reference binding</param>
        public void SetVarName(string localName, string prefix = "", bool recreateBindings = true)
        {
            _varName = new Tokenizer.VarName(prefix, localName);
            if ((_varRef != null) && recreateBindings)
            {
                Bind();
            }
        }

        public override object Execute(IContextProvider provider, object[] dataPool)
        {
            return new ForIterator(this, provider, dataPool,
                XPath2NodeIterator.Create(this[0].Execute(provider, dataPool)));
        }

        public override XPath2ResultType GetReturnType(object[] dataPool)
        {
            return XPath2ResultType.NodeSet;
        }

        private bool MoveNext(IContextProvider provider, object[] dataPool, XPathItem curr, out object res)
        {
            if (curr.IsNode)
                _varRef.Set(dataPool, curr);
            else
                _varRef.Set(dataPool, curr.TypedValue);
            res = this[1].Execute(provider, dataPool);
            if (res != Undefined.Value)
                return true;
            return false;
        }

        /// <inheritdoc/>
        public override string Render()
        {
            return RenderWithKeywords("for", "return");
        }

        public string RenderWithKeywords(string operatorKeyword, string resultKeyword)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(operatorKeyword + " "); // for
            AbstractNode resultNode = null;
            sb.Append(this.RenderForClauseOperator());
            for (int i = 1; i < this.Count; i++)
            {
                if (this[i] is ForNode)
                {
                    sb.Append(", ");
                    sb.Append(((ForNode)this[i]).RenderForClauseOperator());
                    if (this[i].Count > 1)
                    {
                        resultNode = this[i][1];
                    }
                }
                else
                {
                    resultNode = this[i];
                }
            }
            sb.Append(" " + resultKeyword + " "); // return
            sb.Append(resultNode.Render());
            return sb.ToString();
        }

        private string RenderForClauseOperator()
        {
            return "$" + _varName.ToString() + " in " + this[0].Render();
        }

        private sealed class ForIterator : XPath2NodeIterator
        {
            private readonly ForNode owner;
            private readonly IContextProvider provider;
            private readonly object[] dataPool;
            private readonly XPath2NodeIterator baseIter;
            private XPath2NodeIterator iter;
            private XPath2NodeIterator childIter;

            public ForIterator(ForNode owner, IContextProvider provider, object[] dataPool, XPath2NodeIterator baseIter)
            {
                this.owner = owner;
                this.provider = provider;
                this.dataPool = dataPool;
                this.baseIter = baseIter;
            }

            public override XPath2NodeIterator Clone()
            {
                return new ForIterator(owner, provider, dataPool, baseIter);
            }

            public override XPath2NodeIterator CreateBufferedIterator()
            {
                return new BufferedNodeIterator(this);
            }

            protected override void Init()
            {
                iter = baseIter.Clone();
            }

            protected override XPathItem NextItem()
            {
                while (true)
                {
                    if (childIter != null)
                    {
                        if (childIter.MoveNext())
                            return childIter.Current;
                        else
                            childIter = null;
                    }
                    if (!iter.MoveNext())
                        return null;
                    object res;
                    if (owner.MoveNext(provider, dataPool, iter.Current, out res))
                    {
                        childIter = res as XPath2NodeIterator;
                        if (childIter == null)
                        {
                            XPathItem item = res as XPathItem;
                            if (item != null)
                                return item;
                            return new XPath2Item(res);
                        }
                    }
                }
            }

        }
    }
}