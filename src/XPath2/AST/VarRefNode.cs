// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.


using System.Xml;

namespace Wmhelp.XPath2.AST
{
    /// <summary>
    /// An AST node that represents a variable reference..
    /// This class is used by XPath.Net internally. It isn't intended for use in application code.
    /// </summary>
    public sealed class VarRefNode : AbstractNode
    {
        private Tokenizer.VarName _varName;
        private NameBinder.ReferenceLink _varRef;

        public NameBinder.ReferenceLink VarRef => _varRef;

        public XmlQualifiedName QNVarName
            => QNameParser.Parse(_varName.ToString(), Context.NamespaceManager, Context.NameTable);

        public VarRefNode(XPath2Context context, Tokenizer.VarName varRef)
            : base(context)
        {
            _varName = varRef;
        }

        /// <summary>
        /// Changes the variable name this AST node refers to. This method will attempt to fix the bindings if told to do so.
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

        public override void Bind()
        {
            XmlQualifiedName qname = QNameParser.Parse(_varName.ToString(),
                Context.NamespaceManager, Context.NameTable);
            _varRef = Context.RunningContext.NameBinder.VarIndexByName(qname);
        }

        public override string Render()
        {
            return "$" + _varName.ToString();
        }

        public override object Execute(IContextProvider provider, object[] dataPool)
        {
            return _varRef.Get(dataPool);
        }

        public override XPath2ResultType GetReturnType(object[] dataPool)
        {
            return CoreFuncs.GetXPath2ResultType(_varRef.Get(dataPool));
        }
    }
}