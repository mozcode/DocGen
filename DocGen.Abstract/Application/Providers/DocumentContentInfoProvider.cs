using System.Collections.Generic;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Providers;

namespace DocGen.Abstract.Application.Providers
{
    /// <summary>
    /// Concrete implementation of IDocumentInfoProvider
    /// that uses a DocumentContent internally.
    /// 
    /// NOT: GetAllSections() artık MetaSignatureContent içermiyor,
    /// böylece imza alanı fiziksel olarak sayfanın en altına konumlandırılabilir.
    /// </summary>
    public class DocumentContentInfoProvider : IDocumentInfoProvider
    {
        private readonly IDocumentContent _documentContent;

        public DocumentContentInfoProvider(IDocumentContent documentContent)
        {
            _documentContent = documentContent;
        }

        public IDocumentSection GetHeader()
        {
            return _documentContent.MetaHeaderContent;
        }

        public IDocumentSection GetFooter()
        {
            return _documentContent.MetaFooterContent;
        }

        public IDocumentSection GetBody()
        {
            return _documentContent.BodyContent;
        }

        public IDocumentSection GetSignature()
        {
            return _documentContent.MetaSignatureContent;
        }

        public IEnumerable<IDocumentSection> GetAllSections()
        {
            yield return _documentContent.MetaHeaderContent;
            yield return _documentContent.BodyContent;
            yield return _documentContent.MetaFooterContent;
            // Signature intentionally excluded in the iteration
        }
    }
}
