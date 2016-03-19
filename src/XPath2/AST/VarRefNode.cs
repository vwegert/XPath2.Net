// Microsoft Public License (Ms-PL)
// See the file License.rtf or License.txt for the license details.

// Copyright (c) 2011, Semyon A. Chertkov (semyonc@gmail.com)
// All rights reserved.


using System.Xml;

namespace Wmhelp.XPath2.AST
{
    internal sealed class VarRefNode : AbstractNode
    {
        private readonly Tokenizer.VarName _varName;
        private NameBinder.ReferenceLink _varRef;

        public NameBinder.ReferenceLink VarRef => _varRef;

        public XmlQualifiedName QNVarName
            => QNameParser.Parse(_varName.ToString(), Context.NamespaceManager, Context.NameTable);

        public VarRefNode(XPath2Context context, Tokenizer.VarName varRef)
            : base(context)
        {
            _varName = varRef;
        }

        public override void Bind()
        {
            XmlQualifiedName qname = QNameParser.Parse(_varName.ToString(),
                Context.NamespaceManager, Context.NameTable);
            _varRef = Context.RunningContext.NameBinder.VarIndexByName(qname);
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