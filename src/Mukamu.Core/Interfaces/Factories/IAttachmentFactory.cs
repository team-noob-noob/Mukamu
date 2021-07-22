using Mukamu.Core.Models;

namespace Mukamu.Core.Interfaces.Factories
{
    public interface IAttachmentFactory
    {
        Attachment CreateAttachment(string extension, string filename, byte[] blobData);
    }
}
