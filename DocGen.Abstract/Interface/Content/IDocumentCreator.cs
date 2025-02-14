using DocGen.Abstract.Domain.Content;

namespace DocGen.Abstract.Interface.Content
{
    /// <summary>
    /// Interface for creating documents (PDF, Word, etc.) 
    /// using a document content model.
    /// </summary>
    public interface IDocumentCreator
    {
        /// <summary>
        /// Creates a document using minimal/default behavior.
        /// </summary>
        void CreateDocument(IDocumentContent documentContent);

        /// <summary>
        /// Creates a document with additional options (e.g. background, orientation).
        /// </summary>
        void CreateDocument(IDocumentContent documentContent, DocumentCreateOptions options);
    }
}
