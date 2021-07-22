using System;

namespace Mukamu.Core.Models
{
    // TODO: find a store for images
    /// <summary>Represents a file/image uploaded to a post or message</summary>
    public class Attachment : IEntity
    {
        public Attachment() {}
        public Attachment(string fileName, string extension, byte[] blobData)
        {
            this.FileName = fileName;
            this.Extension = extension;
            this.BlobData = blobData;
        }

        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public byte[] BlobData { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
